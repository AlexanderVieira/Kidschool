using System;

namespace Universal.EBI.Core.Messages.Integration.Child
{
    public class DeletedChildIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }

        public DeletedChildIntegrationEvent(Guid id)
        {
            Id = id;
        }
    }
}
