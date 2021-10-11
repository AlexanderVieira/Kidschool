using System;
using Universal.EBI.Core.Integration.Educator;
using Universal.EBI.Educators.API.Models;

namespace Universal.EBI.Educators.API.Integration
{
    public class UpdatedEducatorIntegrationEvent : UpdatedEducatorIntegrationBaseEvent
    {        
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }
       
       
    }
}
