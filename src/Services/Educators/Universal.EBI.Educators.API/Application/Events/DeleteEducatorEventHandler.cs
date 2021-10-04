using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Universal.EBI.Educators.API.Application.Events
{
    public class DeleteEducatorEventHandler : INotificationHandler<DeletedEducatorEvent>
    {
        public Task Handle(DeletedEducatorEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
