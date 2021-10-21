﻿using FluentValidation.Results;
using MediatR;
using System;
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
    public class UpdateClassroomCommandHandler : CommandHandler, IRequestHandler<UpdateClassroomCommand, ValidationResult>
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IClassroomQueries _classroomQueries;

        public UpdateClassroomCommandHandler(IClassroomRepository classroomRepository, IClassroomQueries classroomQueries)
        {
            _classroomRepository = classroomRepository;
            _classroomQueries = classroomQueries;
        }

        public async Task<ValidationResult> Handle(UpdateClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var classroomDto = new ClassroomDto
            {
                Id = message.Id,
                Educator = new EducatorDto(),
                Church = message.Church,
                Region = message.Region,
                ClassroomType = message.ClassroomType,
                Actived = message.Actived,
                MeetingTime = message.MeetingTime,
                LastModifiedDate = message.LastModifiedDate,
                LastModifiedBy = message.LastModifiedBy,
                Childs = message.Childs.Length > 0 ? message.Childs.ToDictionary(c => c.Id.ToString()) : new Dictionary<string, ChildDto>()
            };

            var educatorDto = new EducatorDto().ConvertUpdateCommandToEducatorDto(message);
            classroomDto.Educator = educatorDto;

            var classroom = classroomDto.ToConvertClassroom(classroomDto);

            var existingClassroom = await _classroomQueries.GetClassroomById(classroom.Id);

            if (existingClassroom == null)
            {
                AddError("Esta sala não está em uso.");
                return ValidationResult;
            }

            existingClassroom.Region = classroom.Region;
            existingClassroom.Church = classroom.Church;
            existingClassroom.MeetingTime = classroom.MeetingTime;
            existingClassroom.Educator = classroom.Educator;
            existingClassroom.ClassroomType = classroom.ClassroomType;
            existingClassroom.Actived = classroom.Actived;
            existingClassroom.LastModifiedDate = classroom.LastModifiedDate;
            existingClassroom.LastModifiedBy = classroom.LastModifiedBy;

            for (int i = 0; i < classroom.Children.ToList().Count; i++)
            {
                existingClassroom.Children.Add(classroom.Children.ToList()[i]);
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
                    ClassroomType = existingClassroom.ClassroomType.ToString(),
                    Actived = existingClassroom.Actived,
                    MeetingTime = existingClassroom.MeetingTime,
                    LastModifiedDate = existingClassroom.LastModifiedDate.Value.ToShortDateString(),
                    LastModifiedBy = existingClassroom.LastModifiedBy,
                    Childs = existingClassroom.Children.ToArray()

                });
            }            

            return await PersistData(_classroomRepository.UnitOfWork, success);
        }
    }
}
