using System;
using System.Collections.Generic;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Responsibles.API.Models
{
    public abstract class TEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        private List<Event> _notifications;

        //[BsonIgnore]
        public virtual IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        public virtual void AddEvent(Event myEvent)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(myEvent);
        }

        public virtual void RemoveEvent(Event myEvent)
        {
            _notifications?.Remove(myEvent);
        }

        public virtual void ClearEvent()
        {
            _notifications?.Clear();
        }        
       
    }
}
