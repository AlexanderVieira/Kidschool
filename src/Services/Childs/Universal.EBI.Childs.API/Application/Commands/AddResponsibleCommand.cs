using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Validations;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class AddResponsibleCommand : Command
    {
        public AddResponsibleRequestDto Request { get; set; }

        public AddResponsibleCommand(AddResponsibleRequestDto request)
        {
            Request = request;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddResponsibleValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
