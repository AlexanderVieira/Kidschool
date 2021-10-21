using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Core.Messages.Integration.Responsible;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Responsibles.API.Application.Events
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
            return _bus.PublishAsync(new DeletedResponsibleIntegrationEvent 
            { 
                Id = notification.Id
            });
        }
    }
}
