using System;
using Universal.EBI.BFF.Report.API.Models.Enums;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class PersonDto
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
