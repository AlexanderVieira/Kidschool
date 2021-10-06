using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Childs.API.Models
{
    public class Responsible : IAggregateRoot
    {
        //[BsonId]
        //[BsonGuidRepresentation(GuidRepresentation.CSharpLegacy)]
        public Guid Id { get; set; }                
        
        public Guid? ChildId { get; set; }

        public Responsible()
        {
            Id = Guid.NewGuid();
        }
                
    }
}