using System;

namespace Universal.EBI.Core.Messages.Integration.Classroom
{
    public class DeletedClassroomIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        
    }
}
