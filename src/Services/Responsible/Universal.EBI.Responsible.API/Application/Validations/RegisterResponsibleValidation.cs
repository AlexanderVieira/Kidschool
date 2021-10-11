using FluentValidation;
using Universal.EBI.Responsibles.API.Application.Commands;
using System;
using Universal.EBI.Core.Utils;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Responsibles.API.Application.Validations
{
    public class RegisterResponsibleValidation : AbstractValidator<RegisterResponsibleCommand>
    {
        public RegisterResponsibleValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do responsável inválido.");

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("O nome do responsável não foi informado.");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("O sobrenome do responsável não foi informado.");            

            RuleFor(c => c.Cpf)
                .NotEmpty()
                .WithMessage("O CPF do responsável não foi informado.")
                .Must(HasValidCpf)
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

            RuleFor(c => c.Gender)
                .NotEmpty()
                .WithMessage("O sexo do responsável não foi informado.");                

            RuleFor(c => c.Kinship)
                .NotEmpty()
                .WithMessage("O parenteco do responsável não foi informado.");

        }

        public static bool HasValidCpf(string strCpf)
        {
            return Cpf.CpfValid(strCpf);
        }

    }
}
