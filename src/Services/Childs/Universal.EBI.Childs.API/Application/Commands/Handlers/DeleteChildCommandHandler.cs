using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Extensions;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class DeleteChildCommandHandler : CommandHandler, IRequestHandler<DeleteChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public DeleteChildCommandHandler(IChildRepository childRepository, IChildQueries childQueries, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(DeleteChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            
            var existingChild = await _childRepository.GetChildById(message.Id);
            var context = await _childRepository.GetContext();
            
            if (existingChild == null)
            {
                AddError("Criança não encontrado.");
                return ValidationResult;
            }
            
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var result = await _childRepository.DeleteChild(existingChild);
                        if (result)
                        {
                            ValidationResult = await PersistData(_childRepository.UnitOfWork);

                            if (ValidationResult.IsValid)
                            {
                                existingChild.AddEvent(new DeletedChildEvent
                                {
                                    AggregateId = existingChild.Id,
                                    Id = existingChild.Id
                                });

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
           
            return ValidationResult;
        }
    }
}
