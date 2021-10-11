using System;
using Universal.EBI.Core.Messages.Integration;

namespace Universal.EBI.Core.Integration.Child
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
