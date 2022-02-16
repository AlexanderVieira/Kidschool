﻿using Universal.EBI.Core.DomainObjects;
using FluentValidation;
using Universal.EBI.Childs.API.Application.Commands;
using System;

namespace Universal.EBI.Childs.API.Application.Validations
{
    public class DeleteChildValidation : AbstractValidator<DeleteChildCommand>
    {
        public DeleteChildValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da criança inválido.");
            
        }
        
    }
}