using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Childs.API.Application.Commands;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Events.Integration;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Services
{
    public class UpdateChildIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public UpdateChildIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<UpdatedChildIntegrationEvent, ResponseMessage>(async request => await UpdateChild(request));
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

        private async Task<ResponseMessage> UpdateChild(UpdatedChildIntegrationEvent message)
        {            
            var childRequestDto = new ChildRequestDto
            {                
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                FullName = message.FullName,
                Email = message.Email,
                Cpf = message.Cpf,
                //Phones = message.Phones,
                //Address = message.Address,
                BirthDate = DateTime.Parse(message.BirthDate),
                GenderType = message.Gender,
                AgeGroupType = message.AgeGroup,
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                //Responsibles = message.Responsibles
            };

            var command = new UpdateChildCommand(childRequestDto);

            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(command);
            }

            return new ResponseMessage(sucesso);
        }

    }
}