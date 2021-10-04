using System;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Application.Validations;
using Universal.EBI.Educators.API.Models;

namespace Universal.EBI.Educators.API.Application.Commands
{
    public class RegisterEducatorCommand : Command
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

        //public RegisterEducatorCommand(Guid id, string firstName, string lastName, string email, string cpf, Phone[] phones)
        //{
        //    AggregateId = id;
        //    Id = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    Cpf = cpf;
        //    Phones = phones;
        //}

        public override bool IsValid()
        {
            ValidationResult = new RegisterEducatorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
