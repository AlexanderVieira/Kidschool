using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Extensions;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class RegisterChildCommandHandler : CommandHandler, IRequestHandler<RegisterChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public RegisterChildCommandHandler(IChildRepository childRepository, 
                                           IMediatorHandler mediatorHandler, 
                                           IMapper mapper)
        {
            _childRepository = childRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(RegisterChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var child = _mapper.Map<Child>(message.ChildRequest);
            child.FullName = $"{message.ChildRequest.FirstName} {message.ChildRequest.LastName}";
            child.CreatedDate = DateTime.Now.ToLocalTime();
            child.Address.ChildId = child.Id;
            child.Phones.ToList().ForEach(c => c.Child = child);
            child.Responsibles.ToList().ForEach(r => r.FullName = $"{r.FirstName} {r.LastName}");
            child.Responsibles.ToList().ForEach(r => r.CreatedDate = DateTime.Now.ToLocalTime());
            child.Responsibles.ToList().ForEach(r => r.Address.ResponsibleId = r.Id);            
            child.Responsibles.ToList().ForEach(r => r.Phones.ToList().ForEach(p => p.Responsible = r));

            var context = await _childRepository.GetContext();
            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {                        
                        var result = await _childRepository.CreateChild(child);
                        if (result)
                        {
                            ValidationResult = await PersistData(_childRepository.UnitOfWork);
                            
                            if (ValidationResult.IsValid)
                            {
                                //transaction.CreateSavepoint("RegisterChild");                                
                                child.AddEvent(new RegisteredChildEvent(_mapper.Map<ChildRequestDto>(child)));
                                await _mediatorHandler.PublishEvents(context);
                                await transaction.CommitAsync(cancellationToken);
                            }
                            else
                            {
                                await transaction.RollbackAsync(cancellationToken);
                                AddError($"{message.GetType().Name} : Houve um erro ao persistir os dados.");                                
                            }
                        }
                        else
                        {
                            AddError($"{message.GetType().Name} : Houve um erro ao persistir os dados.");
                            await transaction.RollbackAsync(cancellationToken);                            
                        }

                    }
                    catch (DbUpdateException ex)
                    {
                        await transaction.RollbackAsync(cancellationToken);                        
                        AddError($"{ex.GetType().Name} : Houve um erro ao persistir os dados.");
                    }
                    catch (Exception ex)
                    {                        
                        await transaction.RollbackAsync(cancellationToken);
                        AddError($"{ex.GetType().Name} : {ex.Message}");                                        
                    }
                }

            });

            //await _bus.PublishAsync(new RegisteredChildIntegrationEvent { Id = child.Id });
            return ValidationResult;
        }        
    }
}
