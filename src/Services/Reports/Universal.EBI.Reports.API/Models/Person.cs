using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Reports.API.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool Excluded { get; set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string GenderType { get; set; }
        public Address Address { get; set; }
    }
}
