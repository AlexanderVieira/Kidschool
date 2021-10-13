﻿using System;
using Universal.EBI.Core.Messages.Integration;

namespace Universal.EBI.Core.Integration.Child
{
    public class RegisteredChildIntegrationBaseEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }        
        public string BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
        public bool Excluded { get; set; }
        public Guid[] ResponsibleIds { get; set; }
    }
}