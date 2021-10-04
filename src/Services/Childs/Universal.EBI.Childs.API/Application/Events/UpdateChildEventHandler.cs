using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class UpdateChildEventHandler : INotificationHandler<UpdatedChildEvent>
    {
        public Task Handle(UpdatedChildEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
