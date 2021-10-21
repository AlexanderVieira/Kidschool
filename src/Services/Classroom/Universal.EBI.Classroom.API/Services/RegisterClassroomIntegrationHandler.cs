using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Classrooms.API.Application.Commands;
using Universal.EBI.Classrooms.API.Application.DTOs;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.Core.Messages.Integration.Classroom;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Classrooms.API.Services
{
    public class RegisterClassroomIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterClassroomIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<RegisteredClassroomIntegrationEvent, ResponseMessage>(async request => await RegisterClassroom(request));
            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegisterClassroom(RegisteredClassroomIntegrationEvent message)
        {            
            var classroomCommand = new RegisterClassroomCommand
            {
                AggregateId = message.Id,
                Educator = ConvertToEducatorDto(message.Educator),
                Church = message.Church,
                Region = message.Region,
                ClassroomType = message.ClassroomType,
                Actived = message.Actived,
                MeetingTime = message.MeetingTime,
                //Childs = message.Childs

            };

            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(classroomCommand);
            }

            return new ResponseMessage(sucesso);
        }

        public EducatorDto ConvertToEducatorDto(Educator educator)
        {
            var EducatorDto = new EducatorDto
            {
                Id = educator.Id,
                FirstName = educator.FirstName,
                LastName = educator.LastName,
                Email = new Email(educator.Email.Address),
                Cpf = new Cpf(educator.Cpf.Number),
                Phones = new List<PhoneDto>(),
                Address = new AddressDto(),
                BirthDate = educator.BirthDate.ToShortDateString(),
                GenderType = educator.GenderType.ToString(),
                FunctionType = educator.FunctionType.ToString(),
                PhotoUrl = educator.PhotoUrl,
                Excluded = educator.Excluded
            };

            EducatorDto.Address = new AddressDto
            {
                Id = educator.Address.Id,
                PublicPlace = educator.Address.PublicPlace,
                Number = educator.Address.Number,
                Complement = educator.Address.Complement,
                District = educator.Address.District,
                City = educator.Address.City,
                State = educator.Address.State,
                ZipCode = educator.Address.ZipCode,
                ForeingKeyId = educator.Address.EducatorId
            };

            foreach (var phone in educator.Phones)
            {
                EducatorDto.Phones.Add(new PhoneDto
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = phone.PhoneType.ToString(),
                    ForeingKeyId = phone.EducatorId
                });
            }

            return EducatorDto;
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