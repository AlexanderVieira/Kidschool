﻿using System;
using Universal.EBI.Core.Messages.Integration;

namespace Universal.EBI.Responsible.API.Integration
{
    public class DeletedResponsibleIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }

        public DeletedResponsibleIntegrationEvent(Guid id)
        {
            Id = id;
        }
    }
}
