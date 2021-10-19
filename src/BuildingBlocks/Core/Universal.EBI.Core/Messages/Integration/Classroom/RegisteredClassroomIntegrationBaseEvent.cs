using System;
using Universal.EBI.Core.Messages.Integration;

namespace Universal.EBI.Core.Integration.Classroom
{
    public class RegisteredClassroomIntegrationBaseEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string Lunch { get; set; }
        public string ClassroomType { get; set; }
        public string MeetingTime { get; set; }
        public bool Actived { get; set; }
        public string CreatedAt { get; set; }
        public string UpdateAt { get; set; }
    }
}
