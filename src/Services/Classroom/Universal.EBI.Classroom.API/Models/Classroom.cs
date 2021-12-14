using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Classroom : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public ClassroomType ClassroomType { get; set; }
        public string Lunch { get; set; }
        public string MeetingTime { get; set; }
        public virtual Educator Educator { get; set; }
        public virtual ICollection<Child> Children { get; set; }
        public bool Actived { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public ICollection<Event> Notifications { get; set; }

        public Classroom()
        {
            Notifications = new HashSet<Event>();
        }

        public void AddEvent(Event myEvent)
        {
            var _notifications = Notifications ?? new List<Event>();
            _notifications.Add(myEvent);
        }

        public void RemoveEvent(Event myEvent)
        {
            Notifications?.Remove(myEvent);
        }

        public void ClearEvent()
        {
            Notifications?.Clear();
        }

    }
}
