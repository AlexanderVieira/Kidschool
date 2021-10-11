using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Core.Integration.Classroom;

namespace Universal.EBI.Classrooms.API.Integration
{
    public class UpdatedClassroomIntegrationEvent : UpdatedClassroomIntegrationBaseEvent
    {
        public Educator Educator { get; set; }
        public Child[] Childs { get; set; }
        
    }
}
