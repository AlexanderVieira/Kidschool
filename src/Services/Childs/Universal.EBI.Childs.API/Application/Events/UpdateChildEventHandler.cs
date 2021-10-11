using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class UpdateChildEventHandler : INotificationHandler<UpdatedChildEvent>
    {
        private readonly IMessageBus _bus;

        public UpdateChildEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(UpdatedChildEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            //return _bus.PublishAsync(new UpdatedChildIntegrationEvent 
            //{ 
            //    Id = notification.Id 
            //});
        }
    }
}
