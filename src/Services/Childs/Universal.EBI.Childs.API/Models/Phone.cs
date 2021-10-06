using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Childs.API.Models
{
    public class Phone : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Number { get; set; }        
        public PhoneType PhoneType { get; set; }        
        public Guid? ChildId { get; set; }

        public Phone()
        {
            Id = Guid.NewGuid();
        }
        
        //[JsonIgnore]
        //public virtual Child Child { get; set; }

    }
    
}
