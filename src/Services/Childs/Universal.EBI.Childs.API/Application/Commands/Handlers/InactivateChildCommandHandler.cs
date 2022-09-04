﻿using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Extensions;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands.Handlers
{
    public class InactivateChildCommandHandler : CommandHandler, IRequestHandler<InactivateChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public InactivateChildCommandHandler(IChildRepository childRepository, 
                                         IChildQueries childQueries, 
                                         IMediatorHandler mediatorHandler, 
                                         IMapper mapper)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(InactivateChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;            
            
            var inactiveChild = await _childQueries.GetChildById(message.ChildRequest.Id);            
            
            if (inactiveChild == null)
            {
                AddError("Criança não encontrada.");
                return ValidationResult;
            }
                        
            inactiveChild.Inactivate(message.ChildRequest.Excluded);
            inactiveChild.Address.ChildId = message.ChildRequest.Id;
            inactiveChild.Phones.ToList().ForEach(c => c.Child = inactiveChild);
            inactiveChild.Responsibles.ToList().ForEach(r => r.Address.ResponsibleId = r.Id);
            inactiveChild.Responsibles.ToList().ForEach(r => r.Phones.ToList().ForEach(p => p.Responsible = r));
            inactiveChild.Responsibles.ToList().ForEach(r => r.LastModifiedDate = DateTime.Now.ToLocalTime());

            var context = await _childRepository.GetContext();
            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var result = await _childRepository.InactivateChild(inactiveChild);
                        if (result)
                        {
                            ValidationResult = await PersistData(_childRepository.UnitOfWork);

                            if (ValidationResult.IsValid)
                            {
                                //inactiveChild.AddEvent(new InactivatedChildEvent(_mapper.Map<ChildRequestDto>(inactiveChild)));
                                inactiveChild.AddEvent(new InactivatedChildEvent(new ChildRequestDto
                                {
                                    Id = message.ChildRequest.Id,
                                    FirstName = message.ChildRequest.FirstName,
                                    LastName = message.ChildRequest.LastName,
                                    FullName = message.ChildRequest.FullName,
                                    AddressEmail = message.ChildRequest.AddressEmail,
                                    NumberCpf = message.ChildRequest.NumberCpf,
                                    BirthDate = message.ChildRequest.BirthDate,
                                    GenderType = message.ChildRequest.GenderType,
                                    AgeGroupType = message.ChildRequest.AgeGroupType,
                                    PhotoUrl = message.ChildRequest.PhotoUrl,
                                    Excluded = message.ChildRequest.Excluded,
                                    CreatedBy = message.ChildRequest.CreatedBy,
                                    CreatedDate = message.ChildRequest.CreatedDate,
                                    LastModifiedBy = message.ChildRequest.LastModifiedBy,
                                    LastModifiedDate = inactiveChild.LastModifiedDate,
                                    Phones = message.ChildRequest.Phones,
                                    Address = message.ChildRequest.Address,
                                    Responsibles = message.ChildRequest.Responsibles
                                }));

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