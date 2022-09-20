using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using Universal.EBI.Classrooms.API.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Classrooms.API.Data
{
    public class ClassroomContext : IClassroomContext
    {
        private readonly IMediatorHandler _mediatorHandler;
        public IMongoCollection<Classroom> Classrooms { get; }

        public ClassroomContext(IConfiguration configuration, IMediatorHandler mediatorHandler)
        {            
            _mediatorHandler = mediatorHandler;

            if (!BsonClassMap.IsClassMapRegistered(typeof(Classroom)))
            {
                BsonClassMap.RegisterClassMap<Entity>(map =>
                {
                    map.SetIsRootClass(true);
                    map.AutoMap();
                    map.MapProperty(t => t.Id).SetSerializer(new GuidSerializer(BsonType.String));
                    map.UnmapProperty(c => c.Notifications);                    
                });

                BsonClassMap.RegisterClassMap<Classroom>(map =>
                {
                    map.AutoMap();
                    map.MapCreator(c => new Classroom());
                    map.MapProperty(c => c.ClassroomType).SetSerializer(new EnumSerializer<ClassroomType>(BsonType.String));
                });

                BsonClassMap.RegisterClassMap<Responsible>(map =>
                {
                    map.AutoMap();
                    map.MapCreator(c => new Responsible());                    
                    map.MapProperty(r => r.KinshipType).SetSerializer(new EnumSerializer<KinshipType>(BsonType.String));
                    map.MapProperty(r => r.GenderType).SetSerializer(new EnumSerializer<GenderType>(BsonType.String));                    
                });

                BsonClassMap.RegisterClassMap<Phone>(map =>
                {
                    map.AutoMap();
                    map.MapCreator(c => new Phone());
                    map.MapProperty(p => p.Id).SetSerializer(new GuidSerializer(BsonType.String));
                    map.MapProperty(p => p.PhoneType).SetSerializer(new EnumSerializer<PhoneType>(BsonType.String));
                });
            }

            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Classrooms = database.GetCollection<Classroom>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //ClassroomContextSeed.SeedData(Classrooms);
        }    
    }    
}
