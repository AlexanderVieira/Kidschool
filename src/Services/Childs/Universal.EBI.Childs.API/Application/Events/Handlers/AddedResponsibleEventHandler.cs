using AutoMapper;
using MediatR;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class AddedResponsibleEventHandler : INotificationHandler<AddedResponsibleEvent>
    {
        private readonly IMessageBus _bus;
        private readonly IMapper _mapper;
        private readonly ISincDatabase _sinc;

        public AddedResponsibleEventHandler(IMessageBus bus, IMapper mapper, ISincDatabase sinc)
        {
            _bus = bus;
            _mapper = mapper;
            _sinc = sinc;
        }

        public Task Handle(AddedResponsibleEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var child = _mapper.Map<Child>(notification.ChildRequest);                
                var result = _sinc.UpdateChild(child).Result;
                if (!result) throw new ArgumentException("Erro ao tentar sincronizar base de dados.");
            }
            catch (MongoException)
            {
                throw new MongoException("Erro ao tentar sincronizar base de dados.");
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao tentar sincronizar base de dados.");
            }
            //return _bus.PublishAsync(new RegisteredChildIntegrationEvent(notification.Id));
            return Task.CompletedTask;
        }
    }
}
