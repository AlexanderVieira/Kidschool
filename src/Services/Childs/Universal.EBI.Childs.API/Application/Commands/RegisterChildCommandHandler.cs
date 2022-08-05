using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Extensions;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.DomainObjects.Exceptions;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class RegisterChildCommandHandler : CommandHandler, IRequestHandler<RegisterChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public RegisterChildCommandHandler(IChildRepository childRepository, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _childRepository = childRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(RegisterChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var child = _mapper.Map<Child>(message.ChildRequest);
            child.FullName = $"{child.FirstName} {child.LastName}";
            child.CreatedDate = DateTime.Now.ToLocalTime();
                        
            var context = await _childRepository.GetContext();
            //var strategy = context.Database.CreateExecutionStrategy();

            //strategy.Execute(() =>
            //{
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var success = false;
                    var saved = await _childRepository.CreateChild(child);
                    if (saved) 
                    { 
                        ValidationResult = await PersistData(_childRepository.UnitOfWork);
                        success = ValidationResult.IsValid;                        
                    }
                    else
                    {                        
                        await transaction.RollbackAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                    }

                    if (success)
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
                            CreatedDate = message.ChildRequest.CreatedDate.Value,
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
                        await transaction.CommitAsync(cancellationToken);
                    }

                }
                catch (DomainException ex)
                {                    
                    await transaction.CommitAsync(cancellationToken);
                    AddError($"{ex.GetType().Name} : {ex.Message}");
                }

                catch (DbUpdateException ex)
                {   
                    await transaction.RollbackAsync(cancellationToken);
                    //await transaction.CommitAsync(cancellationToken);
                    AddError($"{ex.GetType().Name} : Houve um erro ao persistir os dados.");
                }
                catch (Exception ex)
                {
                    //await transaction.RollbackToSavepointAsync("RegisterChild");
                    await transaction.RollbackAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);                    
                }
            }

            //});           

            //await _bus.PublishAsync(new RegisteredChildIntegrationEvent { Id = child.Id });
            return ValidationResult;
        }
    }
}
