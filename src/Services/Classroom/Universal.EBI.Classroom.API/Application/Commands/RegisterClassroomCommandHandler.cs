using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.Events;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.MessageBus.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class RegisterClassroomCommandHandler : CommandHandler, IRequestHandler<RegisterClassroomCommand, ValidationResult>
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IClassroomQueries _classroomQueries;
        private readonly IAspNetUser _user;
        private readonly IMessageBus _bus;

        public RegisterClassroomCommandHandler(IClassroomRepository classroomRepository, IClassroomQueries classroomQueries, IAspNetUser user, IMessageBus bus)
        {
            _classroomRepository = classroomRepository;
            _classroomQueries = classroomQueries;
            _user = user;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(RegisterClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            message.ClassroomDto.CreatedDate = message.ClassroomDto.CreatedDate ?? DateTime.Now;
            message.ClassroomDto.CreatedBy = message.ClassroomDto.CreatedBy ?? _user.GetUserEmail();

            var classroom = message.ClassroomDto.ToConvertClassroom(message.ClassroomDto);           

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
                    AggregateId = message.ClassroomDto.Id,
                    Id = message.ClassroomDto.Id,
                    Educator = message.ClassroomDto.Educator,
                    Church = message.ClassroomDto.Church,
                    Region = message.ClassroomDto.Region,
                    Lunch = message.ClassroomDto.Lunch,
                    ClassroomType = message.ClassroomDto.ClassroomType.ToString(),
                    Actived = message.ClassroomDto.Actived,
                    MeetingTime = message.ClassroomDto.MeetingTime,
                    CreatedDate = message.ClassroomDto.CreatedDate.Value.ToShortDateString(),
                    CreatedBy = message.ClassroomDto.CreatedBy,
                    LastModifiedDate = message.ClassroomDto.LastModifiedDate.Value.ToShortDateString(),
                    LastModifiedBy = message.ClassroomDto.LastModifiedBy,
                    Childs = message.ClassroomDto.Childs

                });

                //await _bus.PublishAsync(new RegisteredClassroomReportIntegrationEvent
                //{
                //    AggregateId = createdClassroom.Id,
                //    Id = createdClassroom.Id,
                //    Educator = new Educator 
                //    { 
                //        Id = createdClassroom.Educator.Id, 
                //        FirstName = createdClassroom.Educator.FirstName, 
                //        LastName = createdClassroom.Educator.LastName, 
                //        FunctionType = createdClassroom.Educator.FunctionType
                //    },
                //    Church = createdClassroom.Church,
                //    Region = createdClassroom.Region,
                //    Lunch = createdClassroom.Lunch,
                //    ClassroomType = createdClassroom.ClassroomType.ToString(),
                //    Actived = createdClassroom.Actived,
                //    MeetingTime = createdClassroom.MeetingTime,
                //    CreatedDate = createdClassroom.CreatedDate.ToShortDateString(),
                //    CreatedBy = createdClassroom.CreatedBy,
                //    //Childs = createdClassroom.Children.ToArray()

                //});
            }            

            //return await PersistData(_classroomRepository.UnitOfWork);
            return ValidationResult;
        }
        
    }
}
