using System;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Application.Validations;

namespace Universal.EBI.Educators.API.Application.Commands
{
    public class RegisterEducatorCommand : Command
    {
        public Guid Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; } 
        public string Email { get; set; }
        public string Cpf { get; set; }            
        public string BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string GenderType { get; set; }
        public string FunctionType { get; set; }
        public bool Excluded { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }
        public Guid? ClassroomId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new RegisterEducatorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
