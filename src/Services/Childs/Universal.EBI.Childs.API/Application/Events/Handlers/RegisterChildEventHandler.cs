using AutoMapper;
using MediatR;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.DomainObjects.Exceptions;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class RegisterChildEventHandler : INotificationHandler<RegisteredChildEvent>
    {
        private readonly IMessageBus _bus;
        private readonly IMapper _mapper;        
        private readonly ISincDatabase _childNoSqlRepository;

        public RegisterChildEventHandler(IMessageBus bus, 
                                         IMapper mapper,                                         
                                         ISincDatabase childNoSqlRepository)
        {
            _bus = bus;
            _mapper = mapper;            
            _childNoSqlRepository = childNoSqlRepository;
        }

        public Task Handle(RegisteredChildEvent notification, CancellationToken cancellationToken)
        {              
            try
            {
                var child = _mapper.Map<Child>(notification.ChildRequest);                
                var result = _childNoSqlRepository.CreateChild(child).Result;
                if (result == null) throw new ArgumentException("Erro ao tentar sincronizar base de dados.");
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
