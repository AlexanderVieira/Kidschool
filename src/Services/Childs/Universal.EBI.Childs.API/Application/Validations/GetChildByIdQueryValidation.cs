using FluentValidation;
using System;
using Universal.EBI.Childs.API.Application.Queries;

namespace Universal.EBI.Childs.API.Application.Validations
{
    public class GetChildByIdQueryValidation : AbstractValidator<GetChildByIdQuery>
    {
        public GetChildByIdQueryValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da criança inválido.");
        }
    }
}
