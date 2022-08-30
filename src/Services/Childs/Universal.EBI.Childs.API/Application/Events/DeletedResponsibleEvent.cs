using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class DeletedResponsibleEvent : Event
    {
        public ChildRequestDto ChildRequest { get; set; }

        public DeletedResponsibleEvent(ChildRequestDto childRequest)
        {
            AggregateId = childRequest.Id;
            ChildRequest = childRequest;
        }

    }
}
