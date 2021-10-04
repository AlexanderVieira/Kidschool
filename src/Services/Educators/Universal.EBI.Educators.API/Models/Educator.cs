using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Educators.API.Models
{
    public class Educator : Person, IAggregateRoot
    {               
        public bool Excluded { get; set; }
        public FunctionType? FunctionType { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual Address Address { get; set; }

        // EF Relation
        public Educator()
        {
            Phones = new HashSet<Phone>();
        }

        //public Educator(Guid id, string firstName, string lastName, string email, string cpf, ICollection<Phone> phones)
        //{
        //    Id = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = new Email(email);
        //    Cpf = new Cpf(cpf);
        //    Excluded = false;
        //    Phones = phones;
        //}

        public void ChangeEmail(string email)
        {
            Email = new Email(email);
        }

        public void AssignAddress(Address adddress)
        {
            Address = adddress;
        }
    }

    public abstract class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }               
        public DateTime? BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public Gender? Gender { get; set; }        

    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum FunctionType
    {
        Responsable = 1,
        Auxiliary = 2
    }
}
