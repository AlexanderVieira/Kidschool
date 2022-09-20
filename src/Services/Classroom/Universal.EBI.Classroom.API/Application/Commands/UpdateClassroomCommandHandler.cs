using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.Events;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.MessageBus.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class UpdateClassroomCommandHandler : CommandHandler, IRequestHandler<UpdateClassroomCommand, ValidationResult>,
                                                                 IRequestHandler<AddChildClassroomCommand, ValidationResult>

    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IClassroomQueries _classroomQueries;
        private readonly IAspNetUser _user;
        private readonly IMessageBus _bus;

        public UpdateClassroomCommandHandler(IClassroomRepository classroomRepository, IClassroomQueries classroomQueries, IAspNetUser user, IMessageBus bus)
        {
            _classroomRepository = classroomRepository;
            _classroomQueries = classroomQueries;
            _user = user;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(UpdateClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            message.ClassroomDto.LastModifiedDate = message.ClassroomDto.LastModifiedDate ?? DateTime.Now;
            message.ClassroomDto.LastModifiedBy = message.ClassroomDto.LastModifiedBy ?? _user.GetUserEmail();

            var classroom = message.ClassroomDto.ToConvertClassroom(message.ClassroomDto);

            var existingClassroom = await _classroomQueries.GetClassroomById(classroom.Id);

            if (existingClassroom == null)
            {
                AddError("Esta sala não está em uso.");
                return ValidationResult;
            }

            existingClassroom.Region = classroom.Region;
            existingClassroom.Church = classroom.Church;
            existingClassroom.Lunch = classroom.Lunch;
            existingClassroom.MeetingTime = classroom.MeetingTime;
            existingClassroom.Educator = classroom.Educator;
            existingClassroom.ClassroomType = classroom.ClassroomType;
            existingClassroom.Actived = classroom.Actived;
            existingClassroom.LastModifiedDate = classroom.LastModifiedDate;
            existingClassroom.LastModifiedBy = classroom.LastModifiedBy;
            existingClassroom.Children = classroom.Children;

            var success = await _classroomRepository.UpdateClassroom(existingClassroom);

            if (success)
            {
                classroom.AddEvent(new UpdatedClassroomEvent
                {
                    AggregateId = existingClassroom.Id,
                    Id = existingClassroom.Id,
                    Educator = existingClassroom.Educator,
                    Church = existingClassroom.Church,
                    Region = existingClassroom.Region,
                    Lunch = existingClassroom.Lunch,
                    ClassroomType = existingClassroom.ClassroomType.ToString(),
                    Actived = existingClassroom.Actived,
                    MeetingTime = existingClassroom.MeetingTime,
                    LastModifiedDate = existingClassroom.LastModifiedDate.Value.ToShortDateString(),
                    LastModifiedBy = existingClassroom.LastModifiedBy,
                    Childs = existingClassroom.Children.ToArray()

                });

                //await _bus.PublishAsync(new UpdatedClassroomReportIntegrationEvent
                //{
                //    AggregateId = existingClassroom.Id,
                //    Id = existingClassroom.Id,
                //    //Educator = existingClassroom.Educator,
                //    Church = existingClassroom.Church,
                //    Region = existingClassroom.Region,
                //    Lunch = existingClassroom.Lunch,
                //    ClassroomType = existingClassroom.ClassroomType.ToString(),
                //    Actived = existingClassroom.Actived,
                //    MeetingTime = existingClassroom.MeetingTime,
                //    CreatedDate = existingClassroom.CreatedDate.ToShortDateString(),
                //    CreatedBy = existingClassroom.CreatedBy,
                //    //Childs = existingClassroom.Children.ToArray()

                //});
            }

            return ValidationResult;
            //return await PersistData(_classroomRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AddChildClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            message.ClassroomDto.LastModifiedDate = message.ClassroomDto.LastModifiedDate ?? DateTime.Now;
            message.ClassroomDto.LastModifiedBy = message.ClassroomDto.LastModifiedBy ?? _user.GetUserEmail();

            var classroom = message.ClassroomDto.ToConvertClassroom(message.ClassroomDto);

            var existingClassroom = await _classroomQueries.GetClassroomById(classroom.Id);

            if (existingClassroom == null)
            {
                AddError("Esta sala não está em uso.");
                return ValidationResult;
            }

            existingClassroom.Region = classroom.Region;
            existingClassroom.Church = classroom.Church;
            existingClassroom.Lunch = classroom.Lunch;
            existingClassroom.MeetingTime = classroom.MeetingTime;
            existingClassroom.Educator = classroom.Educator;
            existingClassroom.ClassroomType = classroom.ClassroomType;
            existingClassroom.Actived = classroom.Actived;
            existingClassroom.CreatedBy = classroom.CreatedBy;
            existingClassroom.CreatedDate = classroom.CreatedDate;
            existingClassroom.LastModifiedDate = classroom.LastModifiedDate;
            existingClassroom.LastModifiedBy = classroom.LastModifiedBy;

            int i = 0;
            var dictionary = existingClassroom.Children.Count > 0 ? existingClassroom.Children.ToDictionary(c => c.Id.ToString()) : new Dictionary<string, Child>();
            foreach (var item in classroom.Children)
            {
                if (!dictionary.TryGetValue(item.Id.ToString(), out Child child))
                {
                    child = item;
                    existingClassroom.Children.Add(child);
                }
                i++;
            }

            var success = await _classroomRepository.UpdateClassroom(existingClassroom);

            if (success)
            {
                classroom.AddEvent(new UpdatedClassroomEvent
                {
                    AggregateId = existingClassroom.Id,
                    Id = existingClassroom.Id,
                    Educator = existingClassroom.Educator,
                    Church = existingClassroom.Church,
                    Region = existingClassroom.Region,
                    Lunch = existingClassroom.Lunch,
                    ClassroomType = existingClassroom.ClassroomType.ToString(),
                    Actived = existingClassroom.Actived,
                    MeetingTime = existingClassroom.MeetingTime,
                    CreatedBy = existingClassroom.CreatedBy,
                    CreatedDate = existingClassroom.CreatedDate.ToShortDateString(),
                    LastModifiedDate = existingClassroom.LastModifiedDate.Value.ToShortDateString(),
                    LastModifiedBy = existingClassroom.LastModifiedBy,
                    Childs = existingClassroom.Children.ToArray()

                });
            }

            return ValidationResult;
        }
    }
}
