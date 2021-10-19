using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Reports.API.Models.Enums;

namespace Universal.EBI.Reports.API.Models
{
    public class Classroom : Entity, IAggregateRoot
    {        
        public string Region { get; set; }
        public string Church { get; set; }
        public ClassroomType ClassroomType { get; set; }
        public string Lunch { get; set; }
        public string MeetingTime { get; set; }

        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = (value == null ? DateTime.UtcNow : value); }
        }
        public DateTime? UpdatedAt { get; set; }
        public virtual Educator Educator { get; set; }        
        public virtual ICollection<Child> Children { get; set; }        
        public bool Actived { get; set; }
    }
}
