﻿using Universal.EBI.Core.DomainObjects;
using FluentValidation;
using System;
using Universal.EBI.Educators.API.Application.Commands;

namespace Universal.EBI.Educators.API.Application.Validations
{
    public class RegisterEducatorValidation : AbstractValidator<RegisterEducatorCommand>
    {
        public RegisterEducatorValidation()
        {
            //RuleFor(c => c.Id)
            //    .NotEqual(Guid.Empty)
            //    .WithMessage("Id do cliente inválido.");

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado.");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("O sobrenome do cliente não foi informado.");

            RuleFor(c => c.Cpf)
                .Must(HasValidCpf)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.Email)
                .Must(HasValidEmail)
                .WithMessage("O e-mail informado não é válido.");
        }

        private bool HasValidEmail(string email)
        {
            return Email.EmailValid(email);
        }

        protected bool HasValidCpf(string cpf)
        {
            return Cpf.CpfValid(cpf);
        }
    }
}