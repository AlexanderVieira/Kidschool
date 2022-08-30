using FluentValidation;
using Universal.EBI.Childs.API.Application.Commands;
using System;

namespace Universal.EBI.Childs.API.Application.Validations
{
    public class DeleteResponsibleValidation : AbstractValidator<DeleteResponsibleCommand>
    {
        public DeleteResponsibleValidation()
        {
            RuleFor(c => c.Request.ChildId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do responsável inválido.");
            
        }
        
    }
}
