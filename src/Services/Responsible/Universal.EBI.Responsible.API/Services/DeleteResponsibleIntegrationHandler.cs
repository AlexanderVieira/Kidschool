using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Responsibles.API.Application.Commands;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.MessageBus.Interfaces;
using Universal.EBI.Core.Messages.Integration.Responsible;

namespace Universal.EBI.Responsibles.API.Services
{
    public class DeleteResponsibleIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public DeleteResponsibleIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<DeletedResponsibleIntegrationEvent, ResponseMessage>(async request => await DeleteChild(request));
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

        private async Task<ResponseMessage> DeleteChild(DeletedResponsibleIntegrationEvent message)
        {            
            var ChildCommand = new DeleteResponsibleCommand
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