using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Responsibles.API.Integration;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Responsibles.API.Application.Events
{
    public class RegisterResponsibleEventHandler : INotificationHandler<RegisteredResponsibleEvent>
    {
        private readonly IMessageBus _bus;

        public RegisterResponsibleEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(RegisteredResponsibleEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            //return _bus.PublishAsync(new RegisteredResponsibleIntegrationEvent { Id = notification.Id });
        }
    }
}
