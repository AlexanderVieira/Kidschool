using System;
using System.Collections.Generic;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class ResponsibleDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        //public bool Excluded { get; set; }
        //public string Email { get; set; }
        public string Cpf { get; set; }
        //public string PhotoUrl { get; set; }
        public DateTime BirthDate { get; set; }
        //public string GenderType { get; set; }
        public string KinshipType { get; set; }
        //public string CreatedBy { get; set; }        
        //public AddressDto Address { get; set; }
        public List<PhoneDto> Phones { get; set; }

    }
}