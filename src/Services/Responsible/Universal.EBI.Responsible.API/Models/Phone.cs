using System;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Responsible.API.Models
{
    public class Phone : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Number { get; set; }        
        public PhoneType PhoneType { get; set; }        
        public Guid? ResponsibleId { get; set; }

        public Phone()
        {
            Id = Guid.NewGuid();
        }
        
        //[JsonIgnore]
        //public virtual Child Child { get; set; }

    }
    
}
