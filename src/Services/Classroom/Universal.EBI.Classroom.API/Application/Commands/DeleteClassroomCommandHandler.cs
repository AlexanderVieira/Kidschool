using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.Events;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class DeleteClassroomCommandHandler : CommandHandler, IRequestHandler<DeleteClassroomCommand, ValidationResult>
    {
        private readonly IClassroomRepository _ClassroomRepository;
        private readonly IClassroomQueries _ClassroomQueries;

        public DeleteClassroomCommandHandler(IClassroomRepository childRepository, IClassroomQueries childQueries)
        {
            _ClassroomRepository = childRepository;
            _ClassroomQueries = childQueries;
        }

        public async Task<ValidationResult> Handle(DeleteClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var existingClassroom = await _ClassroomQueries.GetClassroomById(message.Id);

            if (existingClassroom == null)
            {
                AddError("Criança não encontrado.");
                return ValidationResult;
            }

            var success = await _ClassroomRepository.DeleteClassroom(existingClassroom.Id);

            existingClassroom.AddEvent(new DeletedClassroomEvent
            {
                AggregateId = message.Id,
                Id = message.Id
            });

             return await PersistData(_ClassroomRepository.UnitOfWork, success);
        }
    }
}
