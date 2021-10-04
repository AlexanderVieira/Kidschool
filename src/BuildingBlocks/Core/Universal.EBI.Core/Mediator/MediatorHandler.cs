using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T myEvent) where T : Event
        {
            await _mediator.Publish(myEvent);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
