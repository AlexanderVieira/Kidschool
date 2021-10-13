using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Events
{
    public class DeleteChildClassroomEventHandler : INotificationHandler<DeletedChildClassroomEvent>
    {
        private readonly IMessageBus _bus;

        public DeleteChildClassroomEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(DeletedChildClassroomEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            //return _bus.PublishAsync(new DeletedChildClassroomIntegrationEvent { Id = notification.Id });
        }
    }
}
