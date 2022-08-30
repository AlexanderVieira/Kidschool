using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class InactivatedChildEvent : Event
    {
        public ChildRequestDto ChildRequest { get; set; }

        public InactivatedChildEvent(ChildRequestDto childRequest)
        {
            AggregateId = childRequest.Id;
            ChildRequest = childRequest;
        }

    }
}
