using System;

namespace Universal.EBI.Childs.API.Models
{
    public class Address : ICloneable
    {
        public Guid Id { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Guid? ChildId { get; set; }
        public Guid? ResponsibleId { get; set; }
        public Child Child { get; set; }
        public Responsible Responsible { get; set; }

        public Address() 
        {
        }

        public object Clone()
        {
            return MemberwiseClone();   
        }
    }
}