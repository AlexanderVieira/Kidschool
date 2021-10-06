using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Models
{
    public abstract class TEntity
    {
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
