using Universal.EBI.Core.DomainObjects;
using FluentValidation;
using Universal.EBI.Educators.API.Application.Commands;
using System;

namespace Universal.EBI.Educators.API.Application.Validations
{
    public class DeleteEducatorValidation : AbstractValidator<DeleteEducatorCommand>
    {
        public DeleteEducatorValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");            
        }
        
    }
}
