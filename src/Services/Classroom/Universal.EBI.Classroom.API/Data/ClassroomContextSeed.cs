using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Classrooms.API.Data
{
    public class ClassroomContextSeed
    {
        public static void SeedData(IMongoCollection<Classroom> ClassroomCollection)
        {
            bool existChild = ClassroomCollection.Find(p => true).Any();
            if (!existChild)
            {
                ClassroomCollection.InsertMany(GetPreconfiguredClassrooms());
            }
        }

        private static IEnumerable<Classroom> GetPreconfiguredClassrooms()
        {
            return new List<Classroom>()
            {
                new Classroom()
                {
                    
                    
                },
                new Classroom()
                {
                    

                },
                new Classroom()
                {
                    

                }
            };
        }
    }
}
