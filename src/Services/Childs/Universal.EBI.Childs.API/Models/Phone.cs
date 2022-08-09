using System;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Childs.API.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string Number { get; set; }        
        public PhoneType PhoneType { get; set; }
        public virtual Child Child { get; set; }
        public virtual Responsible Responsible { get; set; }

        public Phone()
        {            
        }
    }
}
