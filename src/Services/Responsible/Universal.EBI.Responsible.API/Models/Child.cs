using System;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Responsibles.API.Models
{
    public class Child : IAggregateRoot
    {
        //[BsonId]
        //[BsonGuidRepresentation(GuidRepresentation.CSharpLegacy)]
        public Guid Id { get; set; }                
        
        public Guid? ResponsibleId { get; set; }

        public Child()
        {
            //Id = Guid.NewGuid();
        }
                
    }
}