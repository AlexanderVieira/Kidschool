using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class DeleteChildEventHandler : INotificationHandler<DeletedChildEvent>
    {
        private readonly IMessageBus _bus;

        public DeleteChildEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(DeletedChildEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            //return _bus.PublishAsync(new DeletedChildIntegrationEvent(notification.Id));
        }
    }
}
