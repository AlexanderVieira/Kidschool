﻿using System;
using Universal.EBI.Core.Messages.Integration;

namespace Universal.EBI.Core.Integration.Educator
{
    public class RegisteredEducatorIntegrationBaseEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }        
        public string BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string Function { get; set; }
        public bool Excluded { get; set; }
    }
}