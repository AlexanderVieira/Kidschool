using FluentValidation;
using Universal.EBI.Childs.API.Application.Commands;
using System;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.Childs.API.Application.Validations
{
    public class AddResponsibleValidation : AbstractValidator<AddResponsibleCommand>
    {
        public AddResponsibleValidation()
        {
            RuleFor(c => c.Request.ResponsibleDto.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da criança inválido.");

            RuleFor(c => c.Request.ResponsibleDto.FirstName)
                .NotEmpty()
                .WithMessage("O nome da criança não foi informado.");

            RuleFor(c => c.Request.ResponsibleDto.LastName)
                .NotEmpty()
                .WithMessage("O sobrenome da criança não foi informado.");

            RuleFor(c => c.Request.ResponsibleDto.Cpf)
                .Must(ValidationUtils.HasValidCpf)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.Request.ResponsibleDto.Email)
                .Must(ValidationUtils.HasValidEmail)
                .WithMessage("O e-mail informado não é válido.");

            RuleFor(c => c.Request.ResponsibleDto.BirthDate.Date.ToShortDateString())
                .Must(ValidationUtils.HasValidBirthDate)
                .WithMessage("Data de nascimento não informada.");

            RuleFor(c => c.Request.ResponsibleDto.BirthDate)
                .Custom((birthdate, context) =>
                {
                    if (!string.IsNullOrWhiteSpace(birthdate.Date.ToShortDateString()))
                    {
                        if (DateUtils.IsDataInformadaMaiorQueDataAtual(birthdate.Date.ToShortDateString()))
                            context.AddFailure("A data de nascimento informada não é válida.");
                    }
                });

            RuleFor(c => c.Request.ResponsibleDto.GenderType)
                .NotEmpty()
                .WithMessage("O sexo da criança não foi informado.");

            RuleFor(c => c.Request.ResponsibleDto.KinshipType)
                .NotEmpty()
                .WithMessage("A faixa etária da criança não foi informada.");
            
        }
        
    }
}
