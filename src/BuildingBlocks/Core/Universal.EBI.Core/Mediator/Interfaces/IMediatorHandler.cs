using FluentValidation.Results;
using System.Threading.Tasks;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Core.Mediator.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T myEvent) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
