using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class DeleteChildCommandHandler : CommandHandler, IRequestHandler<DeleteChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;

        public DeleteChildCommandHandler(IChildRepository childRepository, IChildQueries childQueries)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
        }

        public async Task<ValidationResult> Handle(DeleteChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var existingChild = await _childQueries.GetChildById(message.Id);

            if (existingChild == null)
            {
                AddError("Criança não encontrado.");
                return ValidationResult;
            }

            var success = await _childRepository.DeleteChild(existingChild.Id);

            existingChild.AddEvent(new DeletedChildEvent
            {
                AggregateId = message.Id,
                Id = message.Id
            });

             return await PersistData(_childRepository.UnitOfWork, success);
        }
    }
}
