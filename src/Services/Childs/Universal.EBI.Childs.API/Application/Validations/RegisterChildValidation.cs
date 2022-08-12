using FluentValidation;
using Universal.EBI.Childs.API.Application.Commands;
using System;
using Universal.EBI.Core.Utils;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Childs.API.Application.Validations
{
    public class RegisterChildValidation : AbstractValidator<RegisterChildCommand>
    {
        public RegisterChildValidation()
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

            RuleFor(c => c.ChildRequest.Responsibles)
                .Custom((responsibles, context) =>
                {
                    foreach (var item in responsibles)
                    {
                        if (item.Id == Guid.Empty) context.AddFailure("Id do responsável inválido");
                        if (string.IsNullOrEmpty(item.FirstName)) context.AddFailure("O nome do responsável não foi informado.");
                        if (string.IsNullOrEmpty(item.LastName)) context.AddFailure("O Sobrenome do responsável não foi informado.");
                        if (string.IsNullOrEmpty(item.AddressEmail)) context.AddFailure("O e-mail do responsável não foi informado.");
                        if (!Email.EmailValid(item.AddressEmail)) context.AddFailure("E-mail do responsável inválido.");
                        if (string.IsNullOrEmpty(item.NumberCpf)) context.AddFailure("O CPF do responsável não foi informado.");
                        if (!Cpf.CpfValid(item.NumberCpf)) context.AddFailure("CPF do responsável inválido.");
                        if (item.Address == null) context.AddFailure("Endereço do responsável não foi informado.");
                        if (!string.IsNullOrWhiteSpace(item.BirthDate.Date.ToShortDateString()))
                        {
                            if (DateUtils.IsDataInformadaMaiorQueDataAtual(item.BirthDate.Date.ToShortDateString()))
                                context.AddFailure("A data de nascimento informada não é válida.");
                        }
                    }
                });
        }        
    }
}
