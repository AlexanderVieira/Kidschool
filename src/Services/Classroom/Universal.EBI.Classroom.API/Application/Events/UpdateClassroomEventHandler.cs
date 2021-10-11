using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Integration;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Events
{
    public class UpdateClassroomEventHandler : INotificationHandler<UpdatedClassroomEvent>
    {
        private readonly IMessageBus _bus;

        public UpdateClassroomEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(UpdatedClassroomEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            //return _bus.PublishAsync(new UpdatedClassroomIntegrationEvent(notification.Id));
        }
    }
}
