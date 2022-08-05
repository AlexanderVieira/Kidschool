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
            RuleFor(c => c.ChildRequest.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da criança inválido.");

            RuleFor(c => c.ChildRequest.FirstName)
                .NotEmpty()
                .WithMessage("O nome da criança não foi informado.");

            RuleFor(c => c.ChildRequest.LastName)
                .NotEmpty()
                .WithMessage("O sobrenome da criança não foi informado.");

            RuleFor(c => c.ChildRequest.NumberCpf)
                .Must(ValidationUtils.HasValidCpf)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.ChildRequest.AddressEmail)
                .Must(ValidationUtils.HasValidEmail)
                .WithMessage("O e-mail informado não é válido.");

            RuleFor(c => c.ChildRequest.BirthDate.Date.ToShortDateString())
                .Must(ValidationUtils.HasValidBirthDate)
                .WithMessage("Data de nascimento não informada.");

            RuleFor(c => c.ChildRequest.BirthDate)
                .Custom((birthdate, context) =>
                {
                    if (!string.IsNullOrWhiteSpace(birthdate.Date.ToShortDateString()))
                    {
                        if (DateUtils.IsDataInformadaMaiorQueDataAtual(birthdate.Date.ToShortDateString()))
                            context.AddFailure("A data de nascimento informada não é válida.");
                    }
                });

            RuleFor(c => c.ChildRequest.GenderType)
                .NotEmpty()
                .WithMessage("O sexo da criança não foi informado.");

            RuleFor(c => c.ChildRequest.AgeGroupType)
                .NotEmpty()
                .WithMessage("A faixa etária da criança não foi informada.");
        }
        
    }
}
