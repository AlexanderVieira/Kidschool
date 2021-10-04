using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Childs.API.Models
{
    public class Child : Person, IAggregateRoot
    {               
        public bool Excluded { get; set; }
        public virtual ICollection<Responsible> Responsibles { get; set; }
        public Guid ResposibleId { get; set; }

        // EF Relation
        protected Child()
        {
            Responsibles = new HashSet<Responsible>();
        }
       
    }
    
    public abstract class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => GetFullName();
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }               
        public DateTime? BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public GenderType Gender { get; set; }

        private string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public int GetAge()
        {
            return 2;
        }

    }

    public enum GenderType
    {
        Male = 1,
        Female = 2
    }

    public enum ClassroomType
    {
        Bercario = 1,
        Maternal = 2,
        Primario = 3,
        Juniores = 4,
        Mista = 5
    }
}
