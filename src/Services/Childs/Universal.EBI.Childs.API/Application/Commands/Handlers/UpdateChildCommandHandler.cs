using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Extensions;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands.Handlers
{
    public class UpdateChildCommandHandler : CommandHandler, IRequestHandler<UpdateChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public UpdateChildCommandHandler(IChildRepository childRepository,
                                         IChildQueries childQueries,
                                         IMediatorHandler mediatorHandler,
                                         IMapper mapper)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(UpdateChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var updateChild = await _childQueries.GetChildById(message.ChildRequest.Id);

            if (updateChild == null)
            {
                AddError("Criança não encontrada.");
                return ValidationResult;
            }

            var child = _mapper.Map<Child>(message.ChildRequest);
            child.LastModifiedDate = DateTime.Now.ToLocalTime();
            child.Address.ChildId = child.Id;
            child.Phones.ToList().ForEach(c => c.Child = child);
            child.Responsibles.ToList().ForEach(r => r.Address.ResponsibleId = r.Id);
            child.Responsibles.ToList().ForEach(r => r.Phones.ToList().ForEach(p => p.Responsible = r));

            updateChild.FirstName = child.FirstName;
            updateChild.LastName = child.LastName;
            updateChild.FullName = child.FullName;
            updateChild.Email = child.Email;
            updateChild.Cpf = child.Cpf;
            updateChild.Phones = child.Phones;
            updateChild.Address = child.Address;
            updateChild.BirthDate = child.BirthDate;
            updateChild.GenderType = child.GenderType;
            updateChild.AgeGroupType = child.AgeGroupType;
            updateChild.PhotoUrl = child.PhotoUrl;
            updateChild.Excluded = child.Excluded;
            updateChild.CreatedDate = child.CreatedDate;
            updateChild.CreatedBy = child.CreatedBy;
            updateChild.LastModifiedBy = child.LastModifiedBy;
            updateChild.LastModifiedDate = child.LastModifiedDate;
            updateChild.Responsibles = child.Responsibles;

            var context = await _childRepository.GetContext();
            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var result = await _childRepository.UpdateChild(updateChild);
                        if (result)
                        {
                            ValidationResult = await PersistData(_childRepository.UnitOfWork);

                            if (ValidationResult.IsValid)
                            {
                                //updateChild.AddEvent(new UpdatedChildEvent(_mapper.Map<ChildRequestDto>(updateChild)));
                                updateChild.AddEvent(new UpdatedChildEvent(new ChildRequestDto
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
