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

            message.ChildRequest.Responsibles.ToList().ForEach(r => r.CreatedDate = DateTime.Now.ToLocalTime());
            var child = _mapper.Map<Child>(message.ChildRequest);            
            child.Address.ChildId = child.Id;
            child.Phones.ToList().ForEach(c => c.Child = child);
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

                                child.AddEvent(new RegisteredChildEvent(new ChildRequestDto
                                {
                                    Id = message.ChildRequest.Id,
                                    FirstName = message.ChildRequest.FirstName,
                                    LastName = message.ChildRequest.LastName,
                                    FullName = child.FullName,
                                    AddressEmail = message.ChildRequest.AddressEmail,
                                    NumberCpf = message.ChildRequest.NumberCpf,
                                    BirthDate = message.ChildRequest.BirthDate,
                                    GenderType = message.ChildRequest.GenderType,
                                    AgeGroupType = message.ChildRequest.AgeGroupType,
                                    PhotoUrl = message.ChildRequest.PhotoUrl,
                                    Excluded = message.ChildRequest.Excluded,
                                    CreatedBy = message.ChildRequest.CreatedBy,
                                    CreatedDate = child.CreatedDate,
                                    LastModifiedBy = message.ChildRequest.LastModifiedBy,
                                    LastModifiedDate = message.ChildRequest.LastModifiedDate,
                                    Phones = message.ChildRequest.Phones,
                                    Address = message.ChildRequest.Address,
                                    Responsibles = message.ChildRequest.Responsibles
                                }));

                                await _mediatorHandler.PublishEvents_v2(context);
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
                        //await transaction.RollbackToSavepointAsync("RegisterChild");
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
