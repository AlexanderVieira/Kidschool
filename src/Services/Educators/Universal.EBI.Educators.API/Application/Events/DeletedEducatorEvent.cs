using System;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Educators.API.Application.Events
{
    public class DeletedEducatorEvent : Event
    {
        public Guid Id { get; set; }        
       
    }
}
