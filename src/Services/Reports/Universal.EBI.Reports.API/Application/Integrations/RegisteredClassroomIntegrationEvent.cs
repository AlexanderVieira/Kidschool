using Universal.EBI.Core.Integration.Classroom;
using Universal.EBI.Reports.API.Models;

namespace Universal.EBI.Reports.API.Integration
{
    public class RegisteredClassroomIntegrationEvent : RegisteredClassroomIntegrationBaseEvent
    {        
        public Educator Educator { get; set; }
        public Child[] Childs { get; set; }
        
    }
}
