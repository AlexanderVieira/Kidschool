using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Validations;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class InactivateChildCommand : Command
    {
        public ChildRequestDto ChildRequest { get; set; }

        public InactivateChildCommand(ChildRequestDto childRequest)
        {
            ChildRequest = childRequest;
        }

        public override bool IsValid()
        {
            ValidationResult = new InactivateChildValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
