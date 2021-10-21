using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Universal.EBI.Responsibles.API.Models.Interfaces;
using Universal.EBI.Responsibles.API.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Responsibles.API.Data
{
    public class ResponsibleContext : IResponsibleContext
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ResponsibleContext(IConfiguration configuration, IMediatorHandler mediatorHandler)
        {
            //MongoServer srv = MongoServer.Create(myConnStr);
            //BsonDocument doc = srv["db"]["products"].FindOneById(ObjectId.Parse("abcdef01234"));
            //BsonValue dimVal = doc["Dimensions"];
            //List<Dimension> d = BsonSerializer.Deserialize<List<Dimension>>(dimVal.ToJson());

            _mediatorHandler = mediatorHandler;

            if (!BsonClassMap.IsClassMapRegistered(typeof(Responsible)))
            {
                BsonClassMap.RegisterClassMap<Responsible>(map =>
                {
                    map.AutoMap();
                    map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));                    

                });
            }

            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Responsibles = database.GetCollection<Responsible>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            ResponsibleContextSeed.SeedData(Responsibles);
        }

        public IMongoCollection<Responsible> Responsibles { get; }

        public async Task<bool> Commit()
        {
            await _mediatorHandler.PublishEvents(this);
            return true;
        }

        public async Task<bool> Commit(bool success)
        {
            if (success) await _mediatorHandler.PublishEvents(this);
            return success;
        }         
        
    }
    
}
