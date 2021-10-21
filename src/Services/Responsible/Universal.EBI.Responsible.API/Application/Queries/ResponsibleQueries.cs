using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Responsibles.API.Application.Queries.Interfaces;
using Universal.EBI.Responsibles.API.Models;
using Universal.EBI.Responsibles.API.Models.Interfaces;

namespace Universal.EBI.Responsibles.API.Application.Queries
{
    public class ResponsibleQueries : IResponsibleQueries
    {
        
        private readonly IResponsibleContext _context;

        public ResponsibleQueries(IResponsibleContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Responsible>> GetResponsibles(int pageSize, int pageIndex, string query = null)
        {
            
            query = string.IsNullOrEmpty(query) ? "" : query;
            var filter = new BsonDocument { { "FullName", new BsonDocument { { "$regex", query }, { "$options", "i" } } } };

            var responsibles = await _context.Responsibles.Find(filter).ToListAsync();

            var total = responsibles.Count;
            var pageResult = new PagedResult<Responsible>
            {
                List = (IEnumerable<Responsible>)responsibles,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };

            return pageResult;
        }

        public async Task<Responsible> GetResponsibleByCpf(string cpf)
        {
            return await _context
                            .Responsibles
                            .Find(c => c.Cpf.Number.Equals(cpf))
                            .FirstOrDefaultAsync();

        }

        public async Task<Responsible> GetResponsibleById(Guid id)
        {
            return await _context
                            .Responsibles
                            .Find(c => c.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Responsible>> GetResponsiblesByName(string name)
        {
            throw new NotImplementedException();
        }
    }
       
}
