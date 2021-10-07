using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Responsible.API.Application.Events;
using Universal.EBI.Responsible.API.Application.Queries.Interfaces;
using Universal.EBI.Responsible.API.Models.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Responsible.API.Application.Commands
{
    public class DeleteResponsibleCommandHandler : CommandHandler, IRequestHandler<DeleteResponsibleCommand, ValidationResult>
    {
        private readonly IResponsibleRepository _responsibleRepository;
        private readonly IResponsibleQueries _responsibleQueries;

        public DeleteResponsibleCommandHandler(IResponsibleRepository childRepository, IResponsibleQueries childQueries)
        {
            _responsibleRepository = childRepository;
            _responsibleQueries = childQueries;
        }

        public async Task<ValidationResult> Handle(DeleteResponsibleCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var existingResponsible = await _responsibleQueries.GetResponsibleById(message.Id);

            if (existingResponsible == null)
            {
                AddError("Criança não encontrado.");
                return ValidationResult;
            }

            var success = await _responsibleRepository.DeleteResponsible(existingResponsible.Id);

            existingResponsible.AddEvent(new DeletedResponsibleEvent
            {
                AggregateId = message.Id,
                Id = message.Id
            });

             return await PersistData(_responsibleRepository.UnitOfWork, success);
        }
    }
}
