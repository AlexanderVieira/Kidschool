using System;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Educators.API.Models.Enums;

namespace Universal.EBI.Educators.API.Models
{
    public abstract class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public Gender? Gender { get; set; }
        public Address Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}
