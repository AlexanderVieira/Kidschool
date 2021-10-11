using System;
using Universal.EBI.Responsibles.API.Models;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Responsibles.API.Application.Events
{
    public class UpdatedResponsibleEvent : Event
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }
        public string BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string Kinship { get; set; }
        public bool Excluded { get; set; }
        public Child[] Childs { get; set; }

    }
}
