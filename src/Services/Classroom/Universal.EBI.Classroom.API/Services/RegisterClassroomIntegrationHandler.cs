using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Classrooms.API.Application.Commands;
using Universal.EBI.Classrooms.API.Integration;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
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
            var ChildCommand = new RegisterClassroomCommand
            {
                AggregateId = message.Id,
                Id = message.Id,
                
            };

            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(ChildCommand);
            }

            return new ResponseMessage(sucesso);
        }

    }
}