using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Childs.API.Models
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
        public Guid? ChildId { get; set; }

        // EF Relation
        //[JsonIgnore]
        //public virtual Child Child { get; protected set; }

        // EF Constructor
        public Address() 
        {
            Id = Guid.NewGuid();
        }

        public Address(string publicPlace, string number, string complement,
                       string district, string zipCode, string city, string state, Guid? child_Id)
        {
            Id = Guid.NewGuid();
            PublicPlace = publicPlace;
            Number = number;
            Complement = complement;
            District = district;
            ZipCode = zipCode;
            City = city;
            State = state;
            ChildId = child_Id;
        }
    }
}