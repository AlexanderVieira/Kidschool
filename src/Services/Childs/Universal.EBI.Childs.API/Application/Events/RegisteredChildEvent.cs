using System;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class RegisteredChildEvent : Event
    {        
        public ChildRequestDto ChildRequest { get; set; }

        public RegisteredChildEvent(ChildRequestDto childRequest)
        {
            AggregateId = childRequest.Id;
            ChildRequest = childRequest;
        }
    }
}
