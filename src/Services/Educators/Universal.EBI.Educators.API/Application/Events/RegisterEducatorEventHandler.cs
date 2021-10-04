using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Universal.EBI.Educators.API.Application.Events
{
    public class RegisterEducatorEventHandler : INotificationHandler<RegisteredEducatorEvent>
    {
        public Task Handle(RegisteredEducatorEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
