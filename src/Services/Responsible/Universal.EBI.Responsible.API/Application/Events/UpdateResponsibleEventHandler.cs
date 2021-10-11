using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Responsibles.API.Integration;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Responsibles.API.Application.Events
{
    public class UpdateResponsibleEventHandler : INotificationHandler<UpdatedResponsibleEvent>
    {
        private readonly IMessageBus _bus;

        public UpdateResponsibleEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(UpdatedResponsibleEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            //return _bus.PublishAsync(new UpdatedResponsibleIntegrationEvent { Id = notification.Id });
        }
    }
}
