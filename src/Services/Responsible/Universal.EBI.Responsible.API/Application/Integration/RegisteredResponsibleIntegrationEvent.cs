using System;
using Universal.EBI.Core.Integration.Responsible;
using Universal.EBI.Responsibles.API.Models;

namespace Universal.EBI.Responsibles.API.Integration
{
    public class RegisteredResponsibleIntegrationEvent : RegisteredResponsibleIntegrationBaseEvent
    {        
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }       
        public Child[] Childs { get; set; } 
        
    }
}
