using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Childs.API.Application.Commands;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Integration;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.Integration.Responsible;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Services
{
    public class RegisterChildIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;        

        public RegisterChildIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<RegisteredChildIntegrationEvent, ResponseMessage>(async request => await RegisterChild(request));            
            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void SetSubscribers()
        {            
            _bus.SubscribeAsync<RegisteredResponsibleIntegrationBaseEvent>("RegisteredResponsible", async message => await RegisteredResponsible(message));
        }

        private async Task<bool> RegisteredResponsible(RegisteredResponsibleIntegrationBaseEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var childQueries = scope.ServiceProvider.GetRequiredService<IChildQueries>();
                var childRepository = scope.ServiceProvider.GetRequiredService<IChildRepository>();
                //var childContext = scope.ServiceProvider.GetRequiredService<IChildContext>();

                Child childReceived = null;
                bool success = false;
                for (int i = 0; i < message.ChildIds.Length; i++)
                {
                    childReceived = await childQueries.GetChildById(message.ChildIds[i]);
                    if (childReceived != null)
                    {                        
                        childReceived.Responsibles.Add(new Responsible { Id = message.Id, ChildId = message.ChildIds[i] });
                        success = await childRepository.UpdateChild(childReceived);
                    }
                    
                }                

                //if (!await childRepository.UnitOfWork.Commit())
                //{
                //    throw new DomainException($"Problemas ao adicionar crianças na lista. ID: {message.Id}");
                //}

                return await Task.FromResult(success);
            }
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

        private async Task<ResponseMessage> RegisterChild(RegisteredChildIntegrationEvent message)
        {            
            var ChildCommand = new RegisterChildCommand
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
                AgeGroup = message.AgeGroup,
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                Responsibles = message.Responsibles
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