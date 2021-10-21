using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Childs.API.Application.Queries
{
    public class ChildQueries : IChildQueries
    {
        //private readonly IChildRepository _childRepository;
        private readonly IChildContext _context;

        public ChildQueries(IChildContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Child>> GetChilds(int pageSize, int pageIndex, string query = null)
        {
            
            query = string.IsNullOrEmpty(query) ? "" : query;
            var filter = new BsonDocument { { "FullName", new BsonDocument { { "$regex", query }, { "$options", "i" } } } };

            var childs = await _context.Childs.Find(filter).ToListAsync();

            var total = childs.Count;
            var pageResult = new PagedResult<Child>
            {
                List = (IEnumerable<Child>)childs,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };

            return pageResult;
        }

        public async Task<Child> GetChildByCpf(string cpf)
        {
            return await _context
                            .Childs
                            .Find(c => c.Cpf.Number.Equals(cpf))
                            .FirstOrDefaultAsync();

        }

        public async Task<Child> GetChildById(Guid id)
        {
            return await _context
                            .Childs
                            .Find(c => c.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Child>> GetChildsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
       
}
