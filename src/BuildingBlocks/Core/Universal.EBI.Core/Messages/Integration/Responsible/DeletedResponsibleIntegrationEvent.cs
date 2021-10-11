using System;
using Universal.EBI.Core.Messages.Integration;

namespace Universal.EBI.Core.Integration.Responsible
{
    public class DeletedResponsibleIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        
    }
}
