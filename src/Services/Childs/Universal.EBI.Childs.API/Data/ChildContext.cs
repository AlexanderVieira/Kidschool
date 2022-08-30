using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Childs.API.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Universal.EBI.Childs.API.Models;
using System;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Childs.API.Data
{
    public class ChildContext : IChildContext
    {
        private readonly IMediatorHandler _mediatorHandler;
        public IMongoCollection<Child> Children { get; }

        public ChildContext(IConfiguration configuration, IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;

            if (!BsonClassMap.IsClassMapRegistered(typeof(Child)))
            {
                BsonClassMap.RegisterClassMap<Entity>(map =>
                {
                    map.SetIsRootClass(true);
                    map.AutoMap();
                    map.MapProperty(t => t.Id).SetSerializer(new GuidSerializer(BsonType.String));
                    map.UnmapProperty(c => c.Notifications);
                   // map.GetMemberMap(c => c.CreatedDate).SetShouldSerializeMethod(
                   //    obj => ((Entity)obj).CreatedDate > new DateTime(1900, 1, 1)
                   //);
                   // map.GetMemberMap(c => c.LastModifiedDate).SetShouldSerializeMethod(
                   //     obj => ((Entity)obj).LastModifiedDate > new DateTime(1900, 1, 1)
                   // );
                });

                BsonClassMap.RegisterClassMap<Child>(map =>
                {
                    map.AutoMap();
                    map.MapCreator(c => new Child());
                    //map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));                   
                    map.MapProperty(c => c.AgeGroupType).SetSerializer(new EnumSerializer<AgeGroupType>(BsonType.String));
                    map.MapProperty(c => c.GenderType).SetSerializer(new EnumSerializer<GenderType>(BsonType.String));                    
                    //map.UnmapProperty(c => c.Notifications);
                    //map.GetMemberMap(c => c.BirthDate).SetShouldSerializeMethod(
                    //    obj => ((Child)obj).BirthDate > new DateTime(1900, 1, 1)
                    //);
                    //map.GetMemberMap(c => c.CreatedDate).SetShouldSerializeMethod(
                    //    obj => ((Child)obj).CreatedDate > new DateTime(1900, 1, 1)
                    //);
                    //map.GetMemberMap(c => c.LastModifiedDate).SetShouldSerializeMethod(
                    //    obj => ((Child)obj).LastModifiedDate > new DateTime(1900, 1, 1)
                    //);

                });

                BsonClassMap.RegisterClassMap<Responsible>(map =>
                {
                    map.AutoMap();
                    map.MapCreator(c => new Responsible());
                    //map.MapProperty(r => r.Id).SetSerializer(new GuidSerializer(BsonType.String));                    
                    map.MapProperty(r => r.KinshipType).SetSerializer(new EnumSerializer<KinshipType>(BsonType.String));
                    map.MapProperty(r => r.GenderType).SetSerializer(new EnumSerializer<GenderType>(BsonType.String));
                    map.UnmapProperty(r => r.Children);
                });

                BsonClassMap.RegisterClassMap<Phone>(map =>
                {
                    map.AutoMap();
                    map.MapCreator(c => new Phone());
                    map.MapProperty(p => p.Id).SetSerializer(new GuidSerializer(BsonType.String));
                    map.MapProperty(p => p.PhoneType).SetSerializer(new EnumSerializer<PhoneType>(BsonType.String));                    
                });

                BsonClassMap.RegisterClassMap<Address>(map =>
                {
                    map.AutoMap();
                    map.MapCreator(c => new Address());
                    map.MapProperty(a => a.Id).SetSerializer(new GuidSerializer(BsonType.String));                    
                });
            }

            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Children = database.GetCollection<Child>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //ChildContextSeed.SeedData(Children);
        }

    }

}
