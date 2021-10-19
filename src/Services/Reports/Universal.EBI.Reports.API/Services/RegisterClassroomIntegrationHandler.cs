using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Core.DomainObjects.Exceptions;
using Universal.EBI.Core.Integration.Classroom;
using Universal.EBI.MessageBus.Interfaces;
using Universal.EBI.Reports.API.Integration;
using Universal.EBI.Reports.API.Models;
using Universal.EBI.Reports.API.Models.Enums;
using Universal.EBI.Reports.API.Models.Interfaces;

namespace Universal.EBI.Reports.API.Services
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
        //private void SetResponder()
        //{
        //    _bus.RespondAsync<RegisteredChildIntegrationEvent, ResponseMessage>(async request => await RegisterChild(request));            
        //    _bus.AdvancedBus.Connected += OnConnect;
        //}

        private void SetSubscribers()
        {            
            _bus.SubscribeAsync<RegisteredClassroomIntegrationEvent>("RegisteredClassroom", async message => await RegisteredClassroom(message));
        }

        private async Task<bool> RegisteredClassroom(RegisteredClassroomIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {                
                var classroomRepository = scope.ServiceProvider.GetRequiredService<IClassroomRepository>();

                var classroom = new Classroom
                {
                    Id = message.Id,
                    Educator = message.Educator,
                    Church = message.Church,
                    Region = message.Region,
                    ClassroomType = (ClassroomType)Enum.Parse(typeof(ClassroomType), message.ClassroomType, true),
                    Actived = message.Actived,
                    MeetingTime = message.MeetingTime,
                    CreatedAt = DateTime.Parse(message.CreatedAt),
                    UpdatedAt = DateTime.Parse(message.UpdateAt),
                    Lunch = message.Lunch,
                    Children = message.Childs
                };

                await classroomRepository.CreateClassroom(classroom);

                //var childReceived = null;
                //bool success = false;
                //for (int i = 0; i < message.ChildIds.Length; i++)
                //{
                //    childReceived = await childQueries.GetChildById(message.ChildIds[i]);
                //    if (childReceived != null)
                //    {                        
                //        childReceived.Responsibles.Add(new Responsible { Id = message.Id, ChildId = message.ChildIds[i] });
                //        success = await childRepository.UpdateChild(childReceived);
                //    }

                //}                
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