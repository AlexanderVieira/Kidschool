using System;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Core.Messages.Integration.Responsible
{
    public class RegisteredResponsibleIntegrationEvent : IntegrationEvent
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
        public string Kinship { get; set; }
        public bool Excluded { get; set; }        
        public Phone[] Phones { get; set; }
        public DomainObjects.Models.Child[] Childs { get; set; } 
        public Address Address { get; set; }        

    }
}
