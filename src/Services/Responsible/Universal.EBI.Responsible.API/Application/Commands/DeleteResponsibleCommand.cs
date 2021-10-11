using System;
using Universal.EBI.Responsibles.API.Application.Validations;
using Universal.EBI.Core.Messages;
namespace Universal.EBI.Responsibles.API.Application.Commands
{
    public class DeleteResponsibleCommand : Command
    {
        public Guid Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteResponsibleValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
