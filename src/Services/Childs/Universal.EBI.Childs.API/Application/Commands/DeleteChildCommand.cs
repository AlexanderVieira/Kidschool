using System;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Validations;
using Universal.EBI.Core.Messages;
namespace Universal.EBI.Childs.API.Application.Commands
{
    public class DeleteChildCommand : Command
    {
        public Guid Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteChildValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
