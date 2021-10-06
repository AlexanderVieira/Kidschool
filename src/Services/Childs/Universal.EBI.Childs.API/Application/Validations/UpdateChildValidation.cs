using FluentValidation;
using Universal.EBI.Childs.API.Application.Commands;
using System;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.Childs.API.Application.Validations
{
    public class UpdateChildValidation : AbstractValidator<UpdateChildCommand>
    {
        public UpdateChildValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado.");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("O sobrenome do cliente não foi informado.");

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
