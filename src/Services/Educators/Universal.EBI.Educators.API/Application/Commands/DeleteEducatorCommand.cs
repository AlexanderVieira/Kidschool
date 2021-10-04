using System;
using Universal.EBI.Core.Messages;
namespace Universal.EBI.Educators.API.Application.Commands
{
    public class DeleteEducatorCommand : Command
    {
        public Guid Id { get; set; }        
        
        //public override bool IsValid()
        //{
        //    ValidationResult = new UpdateEducatorValidation().Validate(this);
        //    return ValidationResult.IsValid;
        //}
    }
}
