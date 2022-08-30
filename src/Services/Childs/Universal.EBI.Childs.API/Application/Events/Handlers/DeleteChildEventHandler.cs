using MediatR;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Application.Events.Handlers
{
    public class DeleteChildEventHandler : INotificationHandler<DeletedChildEvent>
    {
        private readonly IMessageBus _bus;
        private readonly ISincDatabase _childNoSqlRepository;

        public DeleteChildEventHandler(IMessageBus bus, ISincDatabase childNoSqlRepository)
        {
            _bus = bus;
            _childNoSqlRepository = childNoSqlRepository;
        }

        public Task Handle(DeletedChildEvent notification, CancellationToken cancellationToken)
        {
            try
            {   
                var result = _childNoSqlRepository.DeleteChild(notification.Id).Result;
                if (result) throw new ArgumentException("Erro ao tentar sincronizar base de dados.");
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
            return Task.CompletedTask;
            //return _bus.PublishAsync(new DeletedChildIntegrationEvent(notification.Id));
        }
    }
}
