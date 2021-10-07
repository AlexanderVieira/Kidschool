using System;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Responsible.API.Application.Events
{
    public class DeletedResponsibleEvent : Event
    {
        public Guid Id { get; set; }        
       
    }
}
