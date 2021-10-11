using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Responsibles.API.Application.Commands;
using Universal.EBI.Responsibles.API.Integration;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.MessageBus.Interfaces;
using Universal.EBI.Core.Integration.Child;
using Universal.EBI.Responsibles.API.Models.Interfaces;
using Universal.EBI.Responsibles.API.Application.Queries.Interfaces;
using Universal.EBI.Responsibles.API.Models;
using System.Collections.Generic;

namespace Universal.EBI.Responsibles.API.Services
{
    public class RegisterResponsibleIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;
        public ICollection<Guid> GetGuids { get; set; } = new List<Guid>();

        public RegisterResponsibleIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<RegisteredResponsibleIntegrationEvent, ResponseMessage>(async message => await RegisterResponsible(message));
            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<RegisteredChildIntegrationBaseEvent>("RegisteredChild", async message => await RegisteredChild(message));            
        }       

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
            SetSubscribers();
        }

        private async Task<ResponseMessage> RegisterResponsible(RegisteredResponsibleIntegrationEvent message)
        {            
            var ResponsibleCommand = new RegisterResponsibleCommand
            {
                AggregateId = message.Id,
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                FullName = message.FullName,
                Email = message.Email,
                Cpf = message.Cpf,
                Phones = message.Phones,
                Address = message.Address,
                BirthDate = message.BirthDate,
                Gender = message.Gender,
                Kinship = message.Kinship,
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                Childs = message.Childs
            };

            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(ResponsibleCommand);
            }

            return new ResponseMessage(sucesso);
        }

        private async Task<bool> RegisteredChild(RegisteredChildIntegrationBaseEvent message)
        {           

            using (var scope = _serviceProvider.CreateScope())
            {
                var responsibleQueries = scope.ServiceProvider.GetRequiredService<IResponsibleQueries>();
                var responsibleRepository = scope.ServiceProvider.GetRequiredService<IResponsibleRepository>();
                //var responsibleContext = scope.ServiceProvider.GetRequiredService<IResponsibleContext>();
                
                Responsible responsibleReceived = null;
                bool success = false;
                for (int i = 0; i < message.ResponsibleIds.Length; i++)
                {
                    responsibleReceived = await responsibleQueries.GetResponsibleById(message.ResponsibleIds[i]);
                    if (responsibleReceived != null)
                    {
                        responsibleReceived.Childs.Add(new Child { Id = message.Id, ResponsibleId = message.ResponsibleIds[i] });
                        success = await responsibleRepository.UpdateResponsible(responsibleReceived);
                    }
                    
                }                       

                //if (!await responsibleRepository.UnitOfWork.Commit())
                //{
                //    throw new DomainException($"Problemas ao adicionar crianças na lista. ID: {message.Id}");
                //}

                return await Task.FromResult(success);
            }
        }

    }
}