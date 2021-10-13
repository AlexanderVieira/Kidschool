using FluentValidation;
using Universal.EBI.Classrooms.API.Application.Commands;
using System;

namespace Universal.EBI.Classrooms.API.Application.Validations
{
    public class DeleteChildClassroomValidation : AbstractValidator<DeleteChildClassroomCommand>
    {
        public DeleteChildClassroomValidation()
        {
            RuleFor(c => c.ChildId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da criança inválido.");
            
        }
        
    }
}
