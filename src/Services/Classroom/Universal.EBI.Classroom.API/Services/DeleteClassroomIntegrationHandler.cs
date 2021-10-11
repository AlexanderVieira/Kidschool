using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Classrooms.API.Application.Commands;
using Universal.EBI.Core.Integration.Classroom;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Classrooms.API.Services
{
    public class DeleteClassroomIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public DeleteClassroomIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<DeletedClassroomIntegrationEvent, ResponseMessage>(async request => await DeleteClassroom(request));
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

        private async Task<ResponseMessage> DeleteClassroom(DeletedClassroomIntegrationEvent message)
        {            
            var ChildCommand = new DeleteClassroomCommand
            {
                AggregateId = message.Id,
                Id = message.Id                
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