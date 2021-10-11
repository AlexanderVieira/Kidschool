using System;
using Universal.EBI.Classrooms.API.Models.Enums;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Phone : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Number { get; set; }        
        public PhoneType PhoneType { get; set; }        
        public Guid? ForeingKeyId { get; set; }

        public Phone()
        {
            //Id = Guid.NewGuid();
        }               

    }
    
}
