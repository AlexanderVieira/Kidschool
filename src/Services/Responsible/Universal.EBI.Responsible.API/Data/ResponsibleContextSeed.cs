using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Universal.EBI.Responsible.API.Data
{
    public class ResponsibleContextSeed
    {
        public static void SeedData(IMongoCollection<Models.Responsible> ResponsibleCollection)
        {
            bool existChild = ResponsibleCollection.Find(p => true).Any();
            if (!existChild)
            {
                ResponsibleCollection.InsertMany(GetPreconfiguredResponsibles());
            }
        }

        private static IEnumerable<Models.Responsible> GetPreconfiguredResponsibles()
        {
            return new List<Models.Responsible>()
            {
                new Models.Responsible()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alexander",
                    LastName = "Vieira da Silva",
                    FullName = "Alexander Vieira da Silva"
                    
                },
                new Models.Responsible()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Diego",
                    LastName = "Gonçalves",
                    FullName = "Diego Gonçalves"

                },
                new Models.Responsible()
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
