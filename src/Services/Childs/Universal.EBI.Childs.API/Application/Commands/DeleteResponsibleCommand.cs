using System;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Validations;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class DeleteResponsibleCommand : Command
    {        
        public DeleteResponsibleRequestDto Request { get; set; }

        public DeleteResponsibleCommand(DeleteResponsibleRequestDto request)
        {
            Request = request;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteResponsibleValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
