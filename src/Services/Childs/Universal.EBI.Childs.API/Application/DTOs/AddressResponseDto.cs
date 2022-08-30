using System;
using System.Collections.Generic;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class AddressResponseDto
    {
        public Guid Id { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }        

        public AddressResponseDto()
        { 
        }

    }
}