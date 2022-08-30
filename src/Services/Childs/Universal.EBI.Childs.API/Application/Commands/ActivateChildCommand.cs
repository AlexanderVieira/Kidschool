using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Validations;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class ActivateChildCommand : Command
    {
        public ChildRequestDto ChildRequest { get; set; }

        public ActivateChildCommand(ChildRequestDto childRequest)
        {
            ChildRequest = childRequest;
        }

        public override bool IsValid()
        {
            ValidationResult = new ActivateChildValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
