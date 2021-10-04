using System;
using Universal.EBI.Core.Messages.Integration;

namespace Universal.EBI.Educators.API.Integration
{
    public class DeletedEducatorIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }        
        
    }
}
