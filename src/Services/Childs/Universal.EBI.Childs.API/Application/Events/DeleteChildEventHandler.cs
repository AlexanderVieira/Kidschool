using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class DeleteChildEventHandler : INotificationHandler<DeletedChildEvent>
    {
        private readonly IMessageBus _bus;
        private readonly IChildNoSqlRepository _childNoSqlRepository;

        public DeleteChildEventHandler(IMessageBus bus, IChildNoSqlRepository childNoSqlRepository)
        {
            _bus = bus;
            _childNoSqlRepository = childNoSqlRepository;
        }

        public Task Handle(DeletedChildEvent notification, CancellationToken cancellationToken)
        {
            try
            {                
                var result = _childNoSqlRepository.DeleteChild(notification.Id);
            }
            catch (Exception)
            {
                //Debug.WriteLine(ex.Message);                
                throw;
            }
            return Task.CompletedTask;
            //return _bus.PublishAsync(new DeletedChildIntegrationEvent(notification.Id));
        }
    }
}
