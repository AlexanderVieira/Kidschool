using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;


namespace Universal.EBI.Childs.API.Application.Queries
{
    public class ChildQueries : IChildQueries
    {        
        private readonly IChildContext _context;

        public ChildQueries(IChildContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ChildDesignedQuery>> GetChildren(int pageSize, int pageIndex, string query = null)
        {
            query = string.IsNullOrEmpty(query) ? "" : query;
            var filter = new BsonDocument { { "FullName", new BsonDocument { { "$regex", query }, { "$options", "i" } } } };
            var collection = await _context.Children.Find(filter).ToListAsync();
            var children =  collection.Where(x => x.Excluded == false).Select(x => 
                new ChildDesignedQuery 
                { 
                    Id = x.Id, 
                    FullName = x.FullName, 
                    BirthDate = x.BirthDate, 
                    GenderType = x.GenderType
                    
                }).ToList();
            var total = children.Count;
            var pageResult = new PagedResult<ChildDesignedQuery>
            {
                List = children,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };

            return pageResult;
        }

        public async Task<PagedResult<ChildDesignedQuery>> GetChildrenInactives(int pageSize, int pageIndex, string query = null)
        {
            query = string.IsNullOrEmpty(query) ? "" : query;
            var filter = new BsonDocument { { "FullName", new BsonDocument { { "$regex", query }, { "$options", "i" } } } };
            var collection = await _context.Children.Find(filter).ToListAsync();
            var children = collection.Where(x => x.Excluded == true).Select(x =>
               new ChildDesignedQuery
               {
                   Id = x.Id,
                   FullName = x.FullName,
                   BirthDate = x.BirthDate,
                   GenderType = x.GenderType

               }).ToList();
            var total = children.Count;
            var pageResult = new PagedResult<ChildDesignedQuery>
            {
                List = children,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };

            return pageResult;
        }

        public async Task<Child> GetChildByCpf(string cpf)
        {
            var child = await _context.Children.Find(c => c.Cpf.Number.Equals(cpf)).FirstOrDefaultAsync(); 
            return child;
        }

        public async Task<Child> GetChildById(Guid id)
        {
            var child = await _context.Children.Find(c => c.Id == id).FirstOrDefaultAsync();            
            return child;
        }        
    }

}
