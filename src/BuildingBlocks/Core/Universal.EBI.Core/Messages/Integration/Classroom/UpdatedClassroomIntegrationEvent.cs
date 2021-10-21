using System;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Core.Messages.Integration.Classroom
{
    public class UpdatedClassroomIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string Lunch { get; set; }
        public string ClassroomType { get; set; }
        public string MeetingTime { get; set; }
        public bool Actived { get; set; }
        public DomainObjects.Models.Educator Educator { get; set; }
        public DomainObjects.Models.Child[] Childs { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public Address Address { get; set; }

    }
}
