using FluentValidation;
using Universal.EBI.Childs.API.Application.Queries;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.Childs.API.Application.Validations
{
    public class GetChildByCpfQueryValidation : AbstractValidator<GetChildByCpfQuery>
    {
        public GetChildByCpfQueryValidation()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty()
                .WithMessage("O CPF é obrigatório.")
                .Must(ValidationUtils.HasValidCpf)
                .WithMessage("O CPF informado não é válido.");
        }
    }
}
