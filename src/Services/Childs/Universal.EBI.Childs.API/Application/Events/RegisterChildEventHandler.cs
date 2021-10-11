using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class RegisterChildEventHandler : INotificationHandler<RegisteredChildEvent>
    {
        private readonly IMessageBus _bus;

        public RegisterChildEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(RegisteredChildEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            //return _bus.PublishAsync(new RegisteredChildIntegrationEvent(notification.Id));
        }
    }
}
