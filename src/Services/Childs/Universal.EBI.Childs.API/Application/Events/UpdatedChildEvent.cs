using System;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class UpdatedChildEvent : Event
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string GenderType { get; set; }
        public string AgeGroupType { get; set; }
        public bool Excluded { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }
        public Responsible[] Responsibles { get; set; }

    }
}
