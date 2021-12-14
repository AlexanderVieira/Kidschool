using System;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool Excluded { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string PhotoUrl { get; set; }
        public string BirthDate { get; set; }
        public string GenderType { get; set; }
        public AddressDto Address { get; set; }
    }
}
