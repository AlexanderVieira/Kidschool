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
        private readonly IChildNoSqlRepository _childNoSqlRepository;

        public RegisterChildEventHandler(IMessageBus bus, 
                                         IMapper mapper,                                         
                                         IChildNoSqlRepository childNoSqlRepository)
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
                var savedChild = _childNoSqlRepository.CreateChild(child).Result;
                if (savedChild == null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    throw new DomainException("Sincronização das bases de dados realizada com sucesso.");
                }                
            }
            catch (MongoException)
            {
                throw new MongoException("Erro ao tentar sincronizar base de dados.");
            }
            catch (DomainException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao tentar sincronizar base de dados.");
            }
            //return _bus.PublishAsync(new RegisteredChildIntegrationEvent(notification.Id));
        }
    }
}
