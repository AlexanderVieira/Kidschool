using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Events
{
    public class DeleteClassroomEventHandler : INotificationHandler<DeletedClassroomEvent>
    {
        private readonly IMessageBus _bus;

        public DeleteClassroomEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(DeletedClassroomEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            //return _bus.PublishAsync(new DeletedClassroomIntegrationEvent { Id = notification.Id });
        }
    }
}
