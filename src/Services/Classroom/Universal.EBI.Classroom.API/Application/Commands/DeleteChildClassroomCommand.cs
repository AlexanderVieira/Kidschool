using FluentValidation.Results;
using System;
using Universal.EBI.Classrooms.API.Application.Validations;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class DeleteChildClassroomCommand : Command
    {
        public Guid ClassroomId { get; set; }
        public Guid ChildId { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteChildClassroomValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    
}
