using Universal.EBI.Childs.API.Models;
using Universal.EBI.Core.Integration.Child;

namespace Universal.EBI.Childs.API.Integration
{
    public class UpdatedChildIntegrationEvent : RegisteredChildIntegrationBaseEvent
    {        
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }       
        public Responsible[] Responsibles { get; set; }
        
    }
}
