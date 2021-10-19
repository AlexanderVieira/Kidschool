using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Reports.API.Models.Enums;

namespace Universal.EBI.Reports.API.Models
{
    public class Educator : Person, IAggregateRoot
    {
        public FunctionType FunctionType { get; set; }
        public Guid? ClassroomId { get; set; }
        public virtual Address Address { get; set; }        
        public virtual Classroom Classroom { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
    }
}