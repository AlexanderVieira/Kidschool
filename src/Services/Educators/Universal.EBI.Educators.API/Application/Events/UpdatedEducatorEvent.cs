using System;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Models;

namespace Universal.EBI.Educators.API.Application.Events
{
    public class UpdatedEducatorEvent : Event
    {
        public Guid Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public Address Address { get; set; }
        public Phone[] Phones { get; set; }
        public string BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string Function { get; set; }
        public bool Excluded { get; set; }

        //public UpdatedEducatorEvent(Guid id, string firstName, string lastName, string email, string cpf)
        //{
        //    AggregateId = id;
        //    Id = id;
        //    FirstName = firstName;
        //    LastName = LastName;
        //    Email = email;
        //    Cpf = cpf;
        //}
    }
}
