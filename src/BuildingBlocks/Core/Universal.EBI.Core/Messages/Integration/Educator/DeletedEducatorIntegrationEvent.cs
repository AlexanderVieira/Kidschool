using System;
using Universal.EBI.Core.Messages.Integration;

namespace Universal.EBI.Core.Integration.Educator
{
    public class DeletedEducatorIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        
    }
}
