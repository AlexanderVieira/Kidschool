using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.DomainObjects.Interfaces;
using System;

namespace Universal.EBI.Core.DomainObjects.Models
{
    public class Responsible : Person, IAggregateRoot
    {
        public Guid Id { get; set; }
        public KinshipType KinshipType { get; set; }            
        public virtual Address Address { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }        
        public virtual ICollection<Child> Children { get; set; }

    }
}