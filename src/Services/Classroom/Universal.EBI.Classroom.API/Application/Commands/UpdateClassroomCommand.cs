using System;
using Universal.EBI.Classrooms.API.Application.DTOs;
using Universal.EBI.Classrooms.API.Application.Validations;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class UpdateClassroomCommand : Command
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public int ClassroomType { get; set; }
        public string MeetingTime { get; set; }
        public EducatorDto Educator { get; set; }
        public ChildDto[] Childs { get; set; }
        public bool Actived { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateClassroomValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
