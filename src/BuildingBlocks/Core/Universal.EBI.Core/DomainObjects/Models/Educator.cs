using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Core.DomainObjects.Models
{
    public class Educator : Person, IAggregateRoot
    {
        public Guid Id { get; set; }
        public FunctionType FunctionType { get; set; }
        public Guid? ClassroomId { get; set; }
        public virtual Address Address { get; set; }        
        public virtual Classroom Classroom { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
    }
}