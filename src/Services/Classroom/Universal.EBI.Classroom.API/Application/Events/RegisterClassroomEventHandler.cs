using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.DTOs;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Messages.Integration.Classroom;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Events
{
    public class RegisterClassroomEventHandler : INotificationHandler<RegisteredClassroomEvent>
    {
        private readonly IMessageBus _bus;

        public RegisterClassroomEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(RegisteredClassroomEvent notification, CancellationToken cancellationToken)
        {
            //return Task.CompletedTask;
            return _bus.PublishAsync(new RegisteredClassroomIntegrationEvent
            {
                AggregateId = notification.Id,
                Educator = ToConvertEducator(notification.Educator),
                Church = notification.Church,
                Region = notification.Region,
                ClassroomType = notification.ClassroomType,
                Actived = notification.Actived,
                MeetingTime = notification.MeetingTime,
                //Childs = notification.Childs

            });
        }

        public Educator ToConvertEducator(EducatorDto educatorDto)
        {
            var educator = new Educator
            {
                Id = educatorDto.Id,
                FirstName = educatorDto.FirstName,
                LastName = educatorDto.LastName,
                Email = new Email(educatorDto.Email.Address),
                Cpf = new Cpf(educatorDto.Cpf.Number),
                Phones = new List<Phone>(),
                Address = new Address(),
                BirthDate = DateTime.Parse(educatorDto.BirthDate).Date,
                GenderType = (GenderType)Enum.Parse(typeof(GenderType), educatorDto.GenderType, true),
                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), educatorDto.FunctionType, true),
                PhotoUrl = educatorDto.PhotoUrl,
                Excluded = educatorDto.Excluded
            };

            educator.Address = new Address
            {
                Id = educatorDto.Address.Id,
                PublicPlace = educatorDto.Address.PublicPlace,
                Number = educatorDto.Address.Number,
                Complement = educatorDto.Address.Complement,
                District = educatorDto.Address.District,
                City = educatorDto.Address.City,
                State = educatorDto.Address.State,
                ZipCode = educatorDto.Address.ZipCode,
                EducatorId = educatorDto.Address.ForeingKeyId
            };

            foreach (var phone in educatorDto.Phones)
            {
                educator.Phones.Add(new Phone
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), phone.PhoneType, true),
                    EducatorId = phone.ForeingKeyId
                });
            }

            return educator;
        }
        
    }
}
