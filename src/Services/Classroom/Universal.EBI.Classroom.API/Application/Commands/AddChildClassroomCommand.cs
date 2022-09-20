using System;
using Universal.EBI.Classrooms.API.Application.DTOs;
using Universal.EBI.Classrooms.API.Application.Validations;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class AddChildClassroomCommand : Command
    {        
        public ClassroomDto ClassroomDto { get; set; }

        public AddChildClassroomCommand(ClassroomDto classroomDto)
        {
            ClassroomDto = classroomDto;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddChildClassroomValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
