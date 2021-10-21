using System;

namespace Universal.EBI.Core.Messages.Integration.Educator
{
    public class DeletedEducatorIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        
    }
}
