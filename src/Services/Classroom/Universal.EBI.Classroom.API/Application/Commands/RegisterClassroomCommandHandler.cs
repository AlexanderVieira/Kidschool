using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.DTOs;
using Universal.EBI.Classrooms.API.Application.Events;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class RegisterClassroomCommandHandler : CommandHandler, IRequestHandler<RegisterClassroomCommand, ValidationResult>
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IClassroomQueries _classroomQueries;

        public RegisterClassroomCommandHandler(IClassroomRepository classroomRepository, IClassroomQueries classroomQueries)
        {
            _classroomRepository = classroomRepository;
            _classroomQueries = classroomQueries;
        }

        public async Task<ValidationResult> Handle(RegisterClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var classroomDto = new ClassroomDto
            {
                Id = message.Id,
                Educator = new EducatorDto(),
                Church = message.Church,
                Region = message.Region,
                ClassroomType = message.ClassroomType.ToString(),
                Actived = message.Actived,
                MeetingTime = message.MeetingTime,
                Childs = message.Childs.Length > 0 ? message.Childs.ToDictionary(c => c.Id.ToString()) : new Dictionary<string, ChildDto>()
            };
            
             var educatorDto = new EducatorDto().ConvertRegisterCommandToEducatorDto(message);
            classroomDto.Educator = educatorDto;
            
            var classroom = classroomDto.ToConvertClassroom(classroomDto);           

            var existingClassroom = await _classroomQueries.GetClassroomById(classroom.Id);

            if (existingClassroom != null)
            {
                AddError("Esta sala já está em uso.");
                return ValidationResult;
            }

            await _classroomRepository.CreateClassroom(classroom);
            var createdClassroom = await _classroomQueries.GetClassroomById(classroom.Id);
            var success = createdClassroom != null;            

            classroom.AddEvent(new RegisteredClassroomEvent 
            { 
                AggregateId = classroom.Id,
                Educator = classroom.Educator,
                Church = classroom.Church,
                Region = classroom.Region,
                ClassroomType = (int)classroom.ClassroomType,
                Actived = classroom.Actived,
                MeetingTime = classroom.MeetingTime,
                Childs = classroom.Childs.ToArray()

            });
            
            return await PersistData(_classroomRepository.UnitOfWork, success);
        }
        
    }
}
