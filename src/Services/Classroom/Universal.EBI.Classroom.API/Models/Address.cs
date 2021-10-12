using System;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Address : IAggregateRoot
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
                
        public Address() 
        {
            //Id = Guid.NewGuid();
        }

        public Address(string publicPlace, string number, string complement, string district, 
                       string zipCode, string city, string state, Guid? foreingKeyId)
        {            
            PublicPlace = publicPlace;
            Number = number;
            Complement = complement;
            District = district;
            ZipCode = zipCode;
            City = city;
            State = state;
            ForeingKeyId = foreingKeyId;
        }
    }
}