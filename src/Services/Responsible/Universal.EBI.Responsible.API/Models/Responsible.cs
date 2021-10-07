using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Responsible.API.Models
{
    public class Responsible : IAggregateRoot
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.CSharpLegacy)]
        public Guid Id { get; set; }

        //[BsonElement("FirstName")]
        public string FirstName { get; set; }

        //[BsonElement("LastName")]
        public string LastName { get; set; }

        //[BsonElement("FullName")]
        public string FullName { get; set; }

        //[BsonElement("Excluded")]
        public bool Excluded { get; set; }

        //[BsonRepresentation(BsonType.Document)]
        public Email Email { get; set; }

        //[BsonRepresentation(BsonType.Document)]
        public Cpf Cpf { get; set; }

        //[BsonElement("PhotoUrl")]
        public string PhotoUrl { get; set; }

        //[BsonRepresentation(BsonType.DateTime)]
        public DateTime? BirthDate { get; set; }

        //[JsonConverter(typeof(JsonStringEnumConverter))]  // System.Text.Json.Serialization
        //[BsonRepresentation(BsonType.String)]         // MongoDB.Bson.Serialization.Attributes
        public KinshipType KinshipType { get; set; }

        //[JsonConverter(typeof(JsonStringEnumConverter))]
        //[BsonRepresentation(BsonType.String)]
        public GenderType GenderType { get; set; }

        //[BsonRepresentation(BsonType.Document)]
        public Address Address { get; set; }

        //[BsonRepresentation(BsonType.Array)]
        public ICollection<Phone> Phones { get; set; }

        //[BsonRepresentation(BsonType.Array)]
        public ICollection<Child> Childs { get; set; }

        //private List<Event> _notifications;

        //[BsonExtraElements]
        public ICollection<Event> Notifications { get; set; }

        public void AddEvent(Event myEvent)
        {
            var _notifications = Notifications ?? new List<Event>();
            _notifications.Add(myEvent);
        }

        public void RemoveEvent(Event myEvent)
        {
            Notifications?.Remove(myEvent);
        }

        public void ClearEvent()
        {
            Notifications?.Clear();
        }


        public Responsible()
        {
            Id = Guid.NewGuid();
            Childs = new HashSet<Child>();
            Phones = new HashSet<Phone>();
            Notifications = new HashSet<Event>();
        }

    }    

    public enum GenderType
    {
        Male = 1,
        Female = 2
    }

    public enum KinshipType
    {
        Dad = 1,
        Mom = 2,
        Grandfather = 3,
        Grandmother = 4,
        Uncle = 5,
        Aunt = 6,
        Brother = 7,
        Sister  = 8

    }    

}
