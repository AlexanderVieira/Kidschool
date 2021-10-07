using FluentValidation;
using System;
using Universal.EBI.Educators.API.Application.Commands;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.Educators.API.Application.Validations
{
    public class RegisterEducatorValidation : AbstractValidator<RegisterEducatorCommand>
    {
        public RegisterEducatorValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do educador inválido.");

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("O nome do educador não foi informado.");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("O sobrenome do educador não foi informado.");

            RuleFor(c => c.Cpf)
                .Must(ValidationUtils.HasValidCpf)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.Email)
                .Must(ValidationUtils.HasValidEmail)
                .WithMessage("O e-mail informado não é válido.");

            RuleFor(c => c.BirthDate)
                .Must(ValidationUtils.HasValidBirthDate)
                .WithMessage("Data de nascimento não informada.");

            RuleFor(c => c.BirthDate)
                .Custom((birthdate, context) =>
                {
                    if (!string.IsNullOrWhiteSpace(birthdate))
                    {
                        if (DateUtils.IsDataInformadaMaiorQueDataAtual(birthdate))
                            context.AddFailure("A data de nascimento informada não é válida.");
                    }
                });
        }
        
    }
}
