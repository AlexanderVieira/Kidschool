using FluentValidation;
using Universal.EBI.Childs.API.Application.Commands;
using System;
using Universal.EBI.Core.Utils;
using Universal.EBI.Childs.API.Models;

namespace Universal.EBI.Childs.API.Application.Validations
{
    public class RegisterChildValidation : AbstractValidator<RegisterChildCommand>
    {
        public RegisterChildValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da criança inválido.");

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("O nome da criança não foi informado.");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("O sobrenome da criança não foi informado.");            

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

            RuleFor(c => c.Gender)
                .NotEmpty()
                .WithMessage("O sexo da criança não foi informado.");

            RuleFor(c => c.AgeGroup)
                .NotEmpty()
                .WithMessage("A faixa etária da criança não foi informada.");

        }

        public static Address ValidateRequestAddress(Address address)
        {
            //return address != null ? new Address(address.PublicPlace, address.Number, address.Complement,
            //                       address.District, address.ZipCode, address.City, address.State, address.ChildId): null;
            return address != null ? new Address
            {
                PublicPlace = address.PublicPlace,
                Number = address.Number,
                Complement = address.Complement,
                District = address.District,
                ZipCode = address.ZipCode,
                City = address.City,
                State = address.State,
                ChildId = address.ChildId

            } : null;
            
        }

    }
}
