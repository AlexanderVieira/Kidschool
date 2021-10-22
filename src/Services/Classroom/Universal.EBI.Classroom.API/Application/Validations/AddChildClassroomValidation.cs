using FluentValidation;
using Universal.EBI.Classrooms.API.Application.Commands;
using System;

namespace Universal.EBI.Classrooms.API.Application.Validations
{
    public class AddChildClassroomValidation : AbstractValidator<AddChildClassroomCommand>
    {
        public AddChildClassroomValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da sala inválido.");

            //RuleFor(c => c.FirstName)
            //    .NotEmpty()
            //    .WithMessage("O nome da criança não foi informado.");

            //RuleFor(c => c.LastName)
            //    .NotEmpty()
            //    .WithMessage("O sobrenome da criança não foi informado.");            

            //RuleFor(c => c.Cpf)
            //    .Must(ValidationUtils.HasValidCpf)
            //    .WithMessage("O CPF informado não é válido.");
           
            //RuleFor(c => c.Email)
            //    .Must(ValidationUtils.HasValidEmail)
            //    .WithMessage("O e-mail informado não é válido.");

            //RuleFor(c => c.BirthDate)
            //    .Must(ValidationUtils.HasValidBirthDate)
            //    .WithMessage("Data de nascimento não informada.");

            //RuleFor(c => c.BirthDate)
            //    .Custom((birthdate, context) =>
            //    {
            //        if (!string.IsNullOrWhiteSpace(birthdate))
            //        {
            //            if (DateUtils.IsDataInformadaMaiorQueDataAtual(birthdate))
            //                context.AddFailure("A data de nascimento informada não é válida.");
            //        }
            //    });
            
        }        
        
    }
}
