using System;
using Universal.EBI.Classrooms.API.Application.Validations;
using Universal.EBI.Core.Messages;
namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class DeleteClassroomCommand : Command
    {
        public Guid Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteClassroomValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
