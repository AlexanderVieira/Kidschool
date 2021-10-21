using System;

namespace Universal.EBI.Core.DomainObjects.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Guid? ChildId { get; set; }
        public Guid? ResponsibleId { get; set; }
        public Guid? EducatorId { get; set; }       
        public virtual Child Child { get; set; }
        public virtual Responsible  Responsible { get; set; }
        public virtual Educator Educator { get; set; }
    }
}