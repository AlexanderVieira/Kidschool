using Universal.EBI.Core.Integration.Responsible;
using Universal.EBI.Responsibles.API.Models;

namespace Universal.EBI.Responsibles.API.Integration
{
    public class UpdatedResponsibleIntegrationEvent : UpdatedResponsibleIntegrationBaseEvent
    {        
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }        
        public Child[] Childs { get; set; }

    }
}
