using System;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public abstract class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool Excluded { get; set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }
        public string PhotoUrl { get; set; }
        public string BirthDate { get; set; }        
        public int GenderType { get; set; }
        public AddressDto Address { get; set; }

    }
}
