using System;
using Universal.EBI.Core.Messages.Integration;

namespace Universal.EBI.Core.Integration.Classroom
{
    public class DeletedClassroomIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        
    }
}
