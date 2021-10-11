using System;
using System.Collections.Generic;
using Universal.EBI.Classrooms.API.Models.Enums;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Classroom : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public ClassroomType ClassroomType { get; set; }      
        public string MeetingTime { get; set; }
        public Educator Educator { get; set; }
        public Dictionary<string, Child> Childs { get; set; }
        public bool Actived { get; set; }        
        public ICollection<Event> Notifications { get; set; }        

        public Classroom()
        {
            Childs = new Dictionary<string, Child>();            
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
