using Universal.EBI.Core.DomainObjects;
using FluentValidation;
using Universal.EBI.Responsible.API.Application.Commands;
using System;

namespace Universal.EBI.Responsible.API.Application.Validations
{
    public class DeleteResponsibleValidation : AbstractValidator<DeleteResponsibleCommand>
    {
        public DeleteResponsibleValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da criança inválido.");
            
        }
        
    }
}
