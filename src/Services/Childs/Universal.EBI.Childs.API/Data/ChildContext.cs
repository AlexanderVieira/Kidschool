using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Childs.API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Childs.API.Extensions;
using System;

namespace Universal.EBI.Childs.API.Data
{
    public sealed class ChildContext : IChildContext
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ChildContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Childs = database.GetCollection<Child>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //CatalogContextSeed.SeedData(Childs);
        }

        public IMongoCollection<Child> Childs { get; }

        public Task<bool> Commit()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Commit(bool success)
        {
            if (success) await _mediatorHandler.PublishEvents(this);
            return success;
        }        
    }
    
}
