using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class RegisterChildEventHandler : INotificationHandler<RegisteredChildEvent>
    {
        public Task Handle(RegisteredChildEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
