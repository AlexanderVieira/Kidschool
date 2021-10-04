﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Application.Commands;
using Universal.EBI.Educators.API.Integration;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Educators.API.Services
{
    public class RegisterEducatorIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterEducatorIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<RegisteredEducatorIntegrationEvent, ResponseMessage>(async request => await RegisterEducator(request));
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

        private async Task<ResponseMessage> RegisterEducator(RegisteredEducatorIntegrationEvent message)
        {
            //var educatorCommand = new RegisterEducatorCommand(message.Id, message.FirstName, message.LastName, message.Email, message.Cpf, message.Phones);
            var educatorCommand = new RegisterEducatorCommand
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                Email = message.Email,
                Cpf = message.Cpf,
                Phones = message.Phones,
                Address = message.Address,
                BirthDate = message.BirthDate,
                Gender = message.Gender,
                Function = message.Function,
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded
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