using FluentValidation;
using Universal.EBI.Responsibles.API.Application.Commands;
using System;

namespace Universal.EBI.Responsibles.API.Application.Validations
{
    public class DeleteResponsibleValidation : AbstractValidator<DeleteResponsibleCommand>
    {
        public DeleteResponsibleValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do responsável inválido.");
            
        }
        
    }
}
