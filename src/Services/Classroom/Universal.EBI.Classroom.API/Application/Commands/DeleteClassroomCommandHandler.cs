using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.Events;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class DeleteClassroomCommandHandler : CommandHandler,
                                                 IRequestHandler<DeleteClassroomCommand, ValidationResult>,
                                                 IRequestHandler<DeleteChildClassroomCommand, ValidationResult>
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IClassroomQueries _classroomQueries;
        private readonly IAspNetUser _user;

        public DeleteClassroomCommandHandler(IClassroomRepository classroomRepository, IClassroomQueries classroomQueries, IAspNetUser user)
        {
            _classroomRepository = classroomRepository;
            _classroomQueries = classroomQueries;
            _user = user;
        }

        public async Task<ValidationResult> Handle(DeleteClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var existingClassroom = await _classroomQueries.GetClassroomById(message.Id);

            if (existingClassroom == null)
            {
                AddError("Sala não encontrada.");
                return ValidationResult;
            }

            var success = await _classroomRepository.DeleteClassroom(existingClassroom.Id);

            if (!success)
            {
                AddError("Houve um erro ao persistir os dados...");
                return ValidationResult;
            }
            existingClassroom.AddEvent(new DeletedClassroomEvent
            {
                AggregateId = message.Id,
                Id = message.Id
            });

            return ValidationResult;
            //return await PersistData(_classroomRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DeleteChildClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            message.LastModifiedDate = message.LastModifiedDate ?? DateTime.Now.ToShortDateString();
            message.LastModifiedBy = message.LastModifiedBy ?? _user.GetUserEmail();

            var existingClassroom = await _classroomQueries.GetClassroomById(message.ClassroomId);

            if (existingClassroom == null)
            {
                AddError("Criança não encontrada.");
                return ValidationResult;
            }
            var dictinary = existingClassroom.Children.ToDictionary(c => c.Id.ToString());
            foreach (var item in dictinary)
            {
                if (dictinary.TryGetValue(message.ChildId.ToString(), out Child child))
                {
                    existingClassroom.Children.Remove(child);
                }
            }

            existingClassroom.LastModifiedDate = DateTime.Parse(message.LastModifiedDate);
            existingClassroom.LastModifiedBy = message.LastModifiedBy;

            var success = await _classroomRepository.UpdateClassroom(existingClassroom);

            existingClassroom.AddEvent(new DeletedChildClassroomEvent
            {
                AggregateId = message.ClassroomId,
                ClassroomId = message.ClassroomId,
                ChildId = message.ChildId
            });

            return ValidationResult;
            //return await PersistData(_classroomRepository.UnitOfWork);
        }
    }
}
