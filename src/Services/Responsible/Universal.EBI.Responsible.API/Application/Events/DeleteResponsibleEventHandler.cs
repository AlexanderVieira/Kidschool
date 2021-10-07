using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Responsible.API.Integration;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Responsible.API.Application.Events
{
    public class DeleteResponsibleEventHandler : INotificationHandler<DeletedResponsibleEvent>
    {
        private readonly IMessageBus _bus;

        public DeleteResponsibleEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(DeletedResponsibleEvent notification, CancellationToken cancellationToken)
        {
            //return Task.CompletedTask;
            return _bus.PublishAsync(new DeletedResponsibleIntegrationEvent(notification.Id));
        }
    }
}
