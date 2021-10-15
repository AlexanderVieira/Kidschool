using System;

namespace Universal.EBI.Reports.API.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Guid? ForeingKeyId { get; set; }
    }
}