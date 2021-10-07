using System;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Application.Validations;
using Universal.EBI.Educators.API.Models;

namespace Universal.EBI.Educators.API.Application.Commands
{
    public class UpdateEducatorCommand : Command
    {
        public Guid Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }        
        public string BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string Function { get; set; }
        public bool Excluded { get; set; }              

        public override bool IsValid()
        {
            ValidationResult = new UpdateEducatorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
