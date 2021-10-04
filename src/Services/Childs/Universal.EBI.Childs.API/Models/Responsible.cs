using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Childs.API.Models
{
    public class Responsible : Person, IAggregateRoot
    {
        public bool Excluded { get; set; }
        public ICollection<Child> Childs { get; set; }

        public Guid ChildId { get; set; }

        public Responsible()
        {
            Childs = new HashSet<Child>();
        }
    }
}