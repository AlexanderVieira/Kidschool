using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universal.EBI.Childs.API.Application.Commands;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Events.Integration;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.DomainObjects.Exceptions;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Services
{
    public class RegisterChildIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;        
        private readonly IServiceProvider _serviceProvider;

        public RegisterChildIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;            
            _serviceProvider = serviceProvider;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<RegisteredChildIntegrationEvent, ResponseMessage>(async request => await RegisterChild(request));            
            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void SetSubscribers()
        {            
            _bus.SubscribeAsync<RegisteredResponsibleIntegrationEvent>("RegisteredResponsible", async message => await RegisteredResponsible(message));
        }

        private async Task<bool> RegisteredResponsible(RegisteredResponsibleIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var childQueries = scope.ServiceProvider.GetRequiredService<IChildQueries>();
                var childRepository = scope.ServiceProvider.GetRequiredService<IChildRepository>();               

                Child childReceived = null;
                bool success = false;
                for (int i = 0; i < message.Childs.Length; i++)
                {
                    childReceived = await childQueries.GetChildById(message.Childs[i].Id);
                    if (childReceived != null)
                    {                        
                        childReceived.Responsibles.Add(new Responsible { Id = message.Id });
                        success = await childRepository.UpdateChild(childReceived);
                    }
                    
                }

                if (!await childRepository.UnitOfWork.Commit())
                {
                    throw new DomainException($"Problemas ao adicionar crianças na lista. ID: {message.Id}");
                }

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
            var childCommand = new RegisterChildCommand(new ChildRequestDto());

            childCommand.AggregateId = message.Id;
            childCommand.ChildRequest.Id = message.Id;
            childCommand.ChildRequest.FirstName = message.FirstName;
            childCommand.ChildRequest.LastName = message.LastName;
            //childCommand.ChildRequest.FullName = message.FullName;
            //childCommand.ChildRequest.AddressEmail = message.Email;
            //childCommand.ChildRequest.NumberCpf = message.Cpf;
            //childCommand.ChildRequest.Phones = _mapper.Map<PhoneDto[]>(message.Phones);
            //childCommand.ChildRequest.Address = _mapper.Map<AddressDto>(message.Address);
            //childCommand.ChildRequest.BirthDate = DateTime.Parse(message.BirthDate);
            //childCommand.ChildRequest.GenderType = message.Gender;
            //childCommand.ChildRequest.AgeGroupType = message.AgeGroup;
            //childCommand.ChildRequest.PhotoUrl = message.PhotoUrl;
            //childCommand.ChildRequest.Excluded = message.Excluded;
            //childCommand.ChildRequest.Responsibles = _mapper.Map<ResponsibleDto[]>(message.Responsibles);

            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(childCommand);
            }

            return new ResponseMessage(sucesso);
        }        

    }
}