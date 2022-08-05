using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Validations;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class UpdateChildCommand : Command
    {
        public ChildRequestDto ChildRequest { get; set; }

        public UpdateChildCommand(ChildRequestDto childRequest)
        {
            ChildRequest = childRequest;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateChildValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
