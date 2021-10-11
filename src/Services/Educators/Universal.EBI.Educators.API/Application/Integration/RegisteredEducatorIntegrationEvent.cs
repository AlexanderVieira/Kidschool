using Universal.EBI.Core.Integration.Educator;
using Universal.EBI.Educators.API.Models;

namespace Universal.EBI.Educators.API.Integration
{
    public class RegisteredEducatorIntegrationEvent : RegisteredEducatorIntegrationBaseEvent
    {
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }               
        
    }
}
