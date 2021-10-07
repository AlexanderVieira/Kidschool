using System;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Application.Validations;

namespace Universal.EBI.Educators.API.Application.Commands
{
    public class DeleteEducatorCommand : Command
    {
        public Guid Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteEducatorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
