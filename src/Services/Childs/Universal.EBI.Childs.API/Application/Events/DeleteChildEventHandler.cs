using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class DeleteChildEventHandler : INotificationHandler<DeletedChildEvent>
    {
        public Task Handle(DeletedChildEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
