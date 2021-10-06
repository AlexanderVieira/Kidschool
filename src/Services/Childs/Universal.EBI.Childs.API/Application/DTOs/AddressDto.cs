using System;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class AddressDto
    {
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }        
        
    }
}