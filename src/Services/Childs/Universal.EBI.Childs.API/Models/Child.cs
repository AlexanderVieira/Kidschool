using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Childs.API.Models
{
    public class Child : Entity, IAggregateRoot, ICloneable
    {     
        public string FirstName { get; set; }                
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool Excluded { get; set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public AgeGroupType AgeGroupType { get; set; }
        public GenderType GenderType { get; set; }       
        public virtual Address Address { get; set; }        
        public virtual IList<Phone> Phones { get; set; }
        public virtual IList<Responsible> Responsibles { get; set; }

        public Child()
        {            
            Responsibles = new List<Responsible>();
            Phones = new List<Phone>();            
        }

        public void Activate(bool excluded) { Excluded = excluded; }

        public void Inactivate(bool excluded) { Excluded = excluded; }        

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
