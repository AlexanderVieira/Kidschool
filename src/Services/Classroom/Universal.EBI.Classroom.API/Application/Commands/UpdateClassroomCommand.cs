using System;
using System.Collections.Generic;
using Universal.EBI.Classrooms.API.Application.DTOs;
using Universal.EBI.Classrooms.API.Application.Validations;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class UpdateClassroomCommand : Command
    {       
        public ClassroomDto ClassroomDto { get; set; }

        public UpdateClassroomCommand(ClassroomDto classroomDto)
        {
            ClassroomDto = classroomDto;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateClassroomValidation().Validate(this);
            return ValidationResult.IsValid;
        }
        
    }
}
