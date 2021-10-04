using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Universal.EBI.Educators.API.Application.Events
{
    public class UpdateEducatorEventHandler : INotificationHandler<UpdatedEducatorEvent>
    {
        public Task Handle(UpdatedEducatorEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
