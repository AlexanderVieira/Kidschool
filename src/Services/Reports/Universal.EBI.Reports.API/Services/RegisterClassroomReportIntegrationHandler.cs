using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Exceptions;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Messages.Integration.Report;
using Universal.EBI.MessageBus.Interfaces;
using Universal.EBI.Reports.API.Models.Interfaces;

namespace Universal.EBI.Reports.API.Services
{
    public class RegisterClassroomReportIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;        

        public RegisterClassroomReportIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        //private void SetResponder()
        //{
        //    _bus.RespondAsync<RegisteredChildIntegrationEvent, ResponseMessage>(async request => await RegisterChild(request));            
        //    _bus.AdvancedBus.Connected += OnConnect;
        //}

        private void SetSubscribers()
        {            
            _bus.SubscribeAsync<RegisteredClassroomReportIntegrationEvent>("RegisteredClassroomReport", async message => await RegisteredClassroom(message));
            _bus.AdvancedBus.Connected += OnConnect;
        }

        private async Task<bool> RegisteredClassroom(RegisteredClassroomReportIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {                
                var classroomRepository = scope.ServiceProvider.GetRequiredService<IClassroomRepository>();

                var classroom = new Classroom
                {
                    Id = message.Id,
                    Educator = new Educator(),
                    Church = message.Church,
                    Region = message.Region,
                    Lunch = message.Lunch,
                    ClassroomType = (ClassroomType)Enum.Parse(typeof(ClassroomType), message.ClassroomType, true),
                    Actived = message.Actived,
                    MeetingTime = message.MeetingTime,
                    CreatedDate = DateTime.Parse(message.CreatedDate),
                    CreatedBy = message.CreatedBy,
                    LastModifiedDate = string.IsNullOrWhiteSpace(message.LastModifiedDate) ? null : DateTime.Parse(message.LastModifiedDate),
                    LastModifiedBy = message.LastModifiedBy,                    
                    Children = new List<Child>()
                };

                classroom.Educator = message.Educator;
                classroom.Educator.Address = message.Educator.Address;
                classroom.Children = message.Childs;

                //classroom.Educator = MappingToEducator(message.Educator);
                //foreach (var item in classroom.Children)
                //{
                //    classroom.Children.Add(item);
                //}

                await classroomRepository.CreateClassroom(classroom);
                             
                var success = true;
                if (!await classroomRepository.UnitOfWork.Commit())
                {
                    success = false;
                    throw new DomainException($"Problemas ao criar sala. ID: {message.Id}");
                }
                return await Task.FromResult(success);
            }
        }       

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //SetResponder();
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            //SetResponder();
            SetSubscribers();
        }

        private Educator MappingToEducator(Educator educator) 
        {
            var newEducator = new Educator
            {
                Id = educator.Id,
                FirstName = educator.FirstName,
                LastName = educator.LastName,
                FullName = educator.FullName,
                Email = new Email(educator.Email.Address),
                Cpf = new Cpf(educator.Cpf.Number),
                Phones = new List<Phone>(),
                Address = new Address(),
                BirthDate = educator.BirthDate,
                GenderType = educator.GenderType,                
                FunctionType = educator.FunctionType,
                PhotoUrl = educator.PhotoUrl,
                Excluded = educator.Excluded,
                CreatedDate = educator.CreatedDate,
                CreatedBy = educator.CreatedBy,
                ClassroomId = educator.ClassroomId
            };

            newEducator.Address = new Address
            {
                Id = educator.Address.Id,
                PublicPlace = educator.Address.PublicPlace,
                Number = educator.Address.Number,
                Complement = educator.Address.Complement,
                District = educator.Address.District,
                City = educator.Address.City,
                State = educator.Address.State,
                ZipCode = educator.Address.ZipCode,
                EducatorId = educator.Address.EducatorId
            };

            foreach (var phone in educator.Phones)
            {
                newEducator.Phones.Add(new Phone
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = phone.PhoneType,
                    EducatorId = phone.EducatorId
                });
            }

            return newEducator;

        }

        //private async Task<ResponseMessage> RegisterChild(RegisteredChildIntegrationEvent message)
        //{            
        //    var ChildCommand = new RegisterChildCommand
        //    {
        //        AggregateId = message.Id,
        //        Id = message.Id,
        //        FirstName = message.FirstName,
        //        LastName = message.LastName,
        //        FullName = message.FullName,
        //        Email = message.Email,
        //        Cpf = message.Cpf,
        //        Phones = message.Phones,
        //        Address = message.Address,
        //        BirthDate = message.BirthDate,
        //        Gender = message.Gender,
        //        AgeGroup = message.AgeGroup,
        //        PhotoUrl = message.PhotoUrl,
        //        Excluded = message.Excluded,
        //        Responsibles = message.Responsibles
        //    };

        //    ValidationResult sucesso;

        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
        //        sucesso = await mediator.SendCommand(ChildCommand);
        //    }

        //    return new ResponseMessage(sucesso);
        //}        

    }
}