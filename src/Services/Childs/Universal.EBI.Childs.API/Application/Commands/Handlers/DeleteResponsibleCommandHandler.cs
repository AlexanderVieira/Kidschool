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
using Universal.EBI.WebAPI.Core.Extensions;

namespace Universal.EBI.Childs.API.Application.Commands.Handlers
{
    public class DeleteResponsibleCommandHandler : CommandHandler, IRequestHandler<DeleteResponsibleCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public DeleteResponsibleCommandHandler(IChildRepository childRepository, 
                                         IChildQueries childQueries, 
                                         IMediatorHandler mediatorHandler, 
                                         IMapper mapper)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(DeleteResponsibleCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;            
            
            var recoveredChild = await _childQueries.GetChildById(message.Request.ChildId);
            if (recoveredChild == null)
            {
                AddError("Criança não encontrada.");
                return ValidationResult;
            }                       
                        
            var responsible = _mapper.Map<Responsible>(message.Request.ResponsibleDto);
           
            var aux = new List<Responsible>();
            var existsResponsible = recoveredChild.Responsibles.Contains(responsible);            
            if (!existsResponsible) { AddError("Responsável não encontrado."); return ValidationResult; }
            
            aux.Add(responsible);
            //recoveredChild.Responsibles.Remove(responsible);
            //var aux1 = new List<Responsible>();
            //aux1.AddRange(recoveredChild.Responsibles);

            //var updateChild = new Child();
            //updateChild = (Child)recoveredChild.Clone();            
            //updateChild.Responsibles.Remove(responsible);

            //updateChild.Responsibles.Clear();
            //updateChild.Responsibles = aux1;
            //updateChild.Address.ChildId = message.Request.ChildId;
            //updateChild.Phones.ToList().ForEach(c => c.Child = updateChild);
            //updateChild.Responsibles.ToList().ForEach(r => r.Address.ResponsibleId = r.Id);
            //updateChild.Responsibles.ToList().ForEach(r => r.Address.Responsible = r);
            //updateChild.Responsibles.ToList().ForEach(r => r.Phones.ToList().ForEach(p => p.Responsible = r));

            var toRemoveResponsibleFromChild = new Child();
            toRemoveResponsibleFromChild = (Child)recoveredChild.Clone();
            toRemoveResponsibleFromChild.Responsibles = aux;

            recoveredChild.Responsibles.Remove(responsible);
            //recoveredChild.Responsibles = aux;

            var context = await _childRepository.GetContext();
            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var result = await _childRepository.DeleteResponsible(toRemoveResponsibleFromChild);
                        if (result)
                        {
                            ValidationResult = await PersistData(_childRepository.UnitOfWork);
                            
                            if (ValidationResult.IsValid)
                            {
                                toRemoveResponsibleFromChild.AddEvent(new DeletedResponsibleEvent(_mapper.Map<ChildRequestDto>(recoveredChild)));
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
