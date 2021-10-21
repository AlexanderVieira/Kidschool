using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.Core.Messages.Integration.Educator;
using Universal.EBI.Educators.API.Application.Commands;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Educators.API.Services
{
    public class DeleteEducatorIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public DeleteEducatorIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<DeletedEducatorIntegrationEvent, ResponseMessage>(async request => await DeleteEducator(request));
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

        private async Task<ResponseMessage> DeleteEducator(DeletedEducatorIntegrationEvent message)
        {            
            var educatorCommand = new DeleteEducatorCommand
            {
                AggregateId = message.Id,
                Id = message.Id                
            };
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(educatorCommand);
            }

            return new ResponseMessage(sucesso);
        }

    }
}