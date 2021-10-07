using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Universal.EBI.Responsible.API.Models.Interfaces;
using Universal.EBI.Responsible.API.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Universal.EBI.Responsible.API.Data
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

            if (!BsonClassMap.IsClassMapRegistered(typeof(Models.Responsible)))
            {
                BsonClassMap.RegisterClassMap<Models.Responsible>(map =>
                {
                    //cm.MapIdMember(m => m.Id).SetOrder(0);
                    //cm.MapMember(m => m.FirstName).SetOrder(1);
                    //cm.MapMember(m => m.LastName).SetOrder(2);
                    //cm.MapMember(m => m.FullName).SetOrder(3);
                    //cm.MapMember(m => m.Excluded).SetOrder(4);
                    //cm.MapMember(m => m.Email).SetOrder(5);
                    //cm.MapMember(m => m.Cpf).SetOrder(6);
                    //cm.MapMember(m => m.PhotoUrl).SetOrder(7);
                    //cm.MapMember(m => m.BirthDate).SetOrder(8);
                    //map.MapMember(m => m.KinshipType).SetOrder(9);
                    //cm.MapMember(m => m.GenderType).SetOrder(10);
                    //cm.MapMember(m => m.Address).SetOrder(11);                    
                    //cm.MapMember(m => m.Phones).SetOrder(12);
                    //map.MapMember(m => m.Childs).SetOrder(13);

                    map.AutoMap();
                    map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
                    //map.MapProperty(x => x.Notifications).SetSerializer(new BsonArraySerializer());

                });
            }

            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Responsibles = database.GetCollection<Models.Responsible>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            ResponsibleContextSeed.SeedData(Responsibles);
        }

        public IMongoCollection<Models.Responsible> Responsibles { get; }

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
