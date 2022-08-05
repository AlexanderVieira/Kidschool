using System;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Childs.API.Models
{
    public class Phone : Entity
    {        
        public string Number { get; set; }        
        public PhoneType PhoneType { get; set; } 

        public Phone()
        {            
        }
    }
}
