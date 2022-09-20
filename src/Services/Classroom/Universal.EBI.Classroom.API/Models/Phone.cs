using System;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string Number { get; set; }        
        public PhoneType PhoneType { get; set; }

        public Phone()
        {            
        }               

    }
    
}
