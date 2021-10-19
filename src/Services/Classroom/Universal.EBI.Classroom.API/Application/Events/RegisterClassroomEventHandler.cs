using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Integration;
using Universal.EBI.Core.Integration.Classroom;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Events
{
    public class RegisterClassroomEventHandler : INotificationHandler<RegisteredClassroomEvent>
    {
        private readonly IMessageBus _bus;

        public RegisterClassroomEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(RegisteredClassroomEvent notification, CancellationToken cancellationToken)
        {
            //return Task.CompletedTask;
            return _bus.PublishAsync(new RegisteredClassroomIntegrationEvent
            {
                AggregateId = notification.Id,
                Educator = notification.Educator,
                Church = notification.Church,
                Region = notification.Region,
                ClassroomType = notification.ClassroomType.ToString(),
                Actived = notification.Actived,
                MeetingTime = notification.MeetingTime,
                Childs = notification.Childs

            });
        }
    }
}
