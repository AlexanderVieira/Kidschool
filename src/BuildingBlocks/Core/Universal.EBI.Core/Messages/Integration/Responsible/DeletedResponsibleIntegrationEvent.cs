using System;

namespace Universal.EBI.Core.Messages.Integration.Responsible
{
    public class DeletedResponsibleIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        
    }
}
