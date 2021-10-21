using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Responsibles.API.Data
{
    public class ResponsibleContextSeed
    {
        public static void SeedData(IMongoCollection<Responsible> ResponsibleCollection)
        {
            bool existChild = ResponsibleCollection.Find(p => true).Any();
            if (!existChild)
            {
                ResponsibleCollection.InsertMany(GetPreconfiguredResponsibles());
            }
        }

        private static IEnumerable<Responsible> GetPreconfiguredResponsibles()
        {
            return new List<Responsible>()
            {
                new Responsible()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alexander",
                    LastName = "Vieira da Silva",
                    FullName = "Alexander Vieira da Silva"
                    
                },
                new Responsible()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Diego",
                    LastName = "Gonçalves",
                    FullName = "Diego Gonçalves"

                },
                new Responsible()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Rodrigo",
                    LastName = "Gonçalves Dias",
                    FullName = "Rodrigo Gonçalves Dias"

                }
            };
        }
    }
}
