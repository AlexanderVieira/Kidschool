using System;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class DeletedChildEvent : Event
    {
        public Guid Id { get; set; }        
    }
}
