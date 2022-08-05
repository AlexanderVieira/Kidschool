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
using Universal.EBI.Core.Messages.Integration.Report;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class RegisterClassroomCommandHandler : CommandHandler, IRequestHandler<RegisterClassroomCommand, ValidationResult>
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IClassroomQueries _classroomQueries;
        private readonly IMessageBus _bus;

        public RegisterClassroomCommandHandler(IClassroomRepository classroomRepository, IClassroomQueries classroomQueries, IMessageBus bus)
        {
            _classroomRepository = classroomRepository;
            _classroomQueries = classroomQueries;
            _bus = bus;
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
                Lunch = message.Lunch,
                ClassroomType = message.ClassroomType.ToString(),
                Actived = message.Actived,
                MeetingTime = message.MeetingTime,
                CreatedDate = message.CreatedDate,
                CreatedBy = message.CreatedBy,
                Childs = message.Childs.Length > 0 ? message.Childs.ToDictionary(c => c.Id.ToString()) : new Dictionary<string, ChildDto>()
            };
            
             var educatorDto = new EducatorDto().ConvertRegisterCommandToEducatorDto(message);
            classroomDto.Educator = educatorDto;
            
            var classroom = classroomDto.ToConvertClassroom(classroomDto);           

            var existingClassroom = await _classroomQueries.GetClassroomsByDateAndMeetingTime(classroom.CreatedDate, classroom.MeetingTime);

            if (existingClassroom != null)
            {
                AddError("Esta sala já está em uso.");
                return ValidationResult;
            }

            await _classroomRepository.CreateClassroom(classroom);
            var createdClassroom = await _classroomQueries.GetClassroomById(classroom.Id);
            var success = createdClassroom != null;
            
            if (success)
            {
                classroom.AddEvent(new RegisteredClassroomEvent
                {
                    AggregateId = classroomDto.Id,
                    Id = classroomDto.Id,
                    Educator = classroomDto.Educator,
                    Church = classroomDto.Church,
                    Region = classroomDto.Region,
                    Lunch = classroomDto.Lunch,
                    ClassroomType = classroomDto.ClassroomType.ToString(),
                    Actived = classroomDto.Actived,
                    MeetingTime = classroomDto.MeetingTime,
                    CreatedDate = classroomDto.CreatedDate,
                    CreatedBy = classroomDto.CreatedBy,  
                    Childs = classroomDto.Childs.Values.ToArray()

                });

                await _bus.PublishAsync(new RegisteredClassroomReportIntegrationEvent
                {
                    AggregateId = createdClassroom.Id,
                    Id = createdClassroom.Id,
                    //Educator = createdClassroom.Educator,
                    Church = createdClassroom.Church,
                    Region = createdClassroom.Region,
                    Lunch = createdClassroom.Lunch,
                    ClassroomType = createdClassroom.ClassroomType.ToString(),
                    Actived = createdClassroom.Actived,
                    MeetingTime = createdClassroom.MeetingTime,
                    CreatedDate = createdClassroom.CreatedDate.ToShortDateString(),
                    CreatedBy = createdClassroom.CreatedBy,
                    Childs = createdClassroom.Children.ToArray()

                });
            }            

            return await PersistData(_classroomRepository.UnitOfWork);
        }
        
    }
}
