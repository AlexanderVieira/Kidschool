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
                BsonClassMap.RegisterClassMap<Classroom>(map =>
                {
                    map.AutoMap();
                    map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
                    
                });
            }

            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Classrooms = database.GetCollection<Classroom>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //ClassroomContextSeed.SeedData(Classrooms);
        }    
    }    
}
