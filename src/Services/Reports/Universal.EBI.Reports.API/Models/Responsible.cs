using System.Collections.Generic;
using Universal.EBI.Classrooms.API.Models.Enums;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Reports.API.Models
{
    public class Responsible : Person, IAggregateRoot
    {
        public KinshipType KinshipType { get; set; }            
        public virtual Address Address { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }        
        public virtual ICollection<Child> Children { get; set; }

    }
}