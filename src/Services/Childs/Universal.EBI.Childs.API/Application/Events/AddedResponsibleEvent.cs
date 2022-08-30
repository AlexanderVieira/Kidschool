using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class AddedResponsibleEvent : Event
    {
        public ChildRequestDto ChildRequest { get; set; }

        public AddedResponsibleEvent(ChildRequestDto childRequest)
        {
            AggregateId = childRequest.Id;
            ChildRequest = childRequest;
        }

    }
}
