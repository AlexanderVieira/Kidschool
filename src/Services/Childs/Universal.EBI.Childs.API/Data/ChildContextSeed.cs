using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Childs.API.Data
{
    public class ChildContextSeed
    {
        public static void SeedData(IMongoCollection<Child> ChildCollection)
        {
            bool existChild = ChildCollection.Find(p => true).Any();
            if (!existChild)
            {
                ChildCollection.InsertMany(GetPreconfiguredChilds());
            }
        }

        private static IEnumerable<Child> GetPreconfiguredChilds()
        {
            return new List<Child>()
            {
                new Child()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jonathan",
                    LastName = "de Sales da Silva",
                    FullName = "Jonathan de Sales da Silva"
                    
                },
                new Child()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Miguel",
                    LastName = "Gonçalves",
                    FullName = "Miguel Gonçalves"

                },
                new Child()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Guilherme",
                    LastName = "Gonçalves Dias",
                    FullName = "Miguel Gonçalves Dias"

                }
            };
        }
    }
}
