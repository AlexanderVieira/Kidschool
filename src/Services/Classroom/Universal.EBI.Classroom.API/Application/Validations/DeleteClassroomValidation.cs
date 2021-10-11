using Universal.EBI.Core.DomainObjects;
using FluentValidation;
using Universal.EBI.Classrooms.API.Application.Commands;
using System;

namespace Universal.EBI.Classrooms.API.Application.Validations
{
    public class DeleteClassroomValidation : AbstractValidator<DeleteClassroomCommand>
    {
        public DeleteClassroomValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da criança inválido.");
            
        }
        
    }
}
