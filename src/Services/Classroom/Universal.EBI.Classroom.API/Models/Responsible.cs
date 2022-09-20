using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Responsible
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        //public bool Excluded { get; set; }
        //public Email Email { get; set; }
        public Cpf Cpf { get; set; }
        //public string PhotoUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderType GenderType { get; set; }
        public KinshipType KinshipType { get; set; }
        //public virtual Address Address { get; set; }
        public virtual IList<Phone> Phones { get; set; }
        //public virtual IList<Child> Children { get; set; }

        public Responsible()
        {
            //Children = new List<Child>();
            Phones = new List<Phone>();
        }

    }     

}
