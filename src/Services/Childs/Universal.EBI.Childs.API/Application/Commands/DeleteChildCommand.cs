using System;
using Universal.EBI.Core.Messages;
namespace Universal.EBI.Childs.API.Application.Commands
{
    public class DeleteChildCommand : Command
    {
        public Guid Id { get; set; }        
        
        //public override bool IsValid()
        //{
        //    ValidationResult = new UpdateEducatorValidation().Validate(this);
        //    return ValidationResult.IsValid;
        //}
    }
}
