using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;

namespace Universal.EBI.Childs.API.Application.Queries
{
    public class ChildQueries : IChildQueries
    {
        private readonly IChildRepository _ChildRepository;

        public ChildQueries(IChildRepository childRepository)
        {
            _ChildRepository = childRepository;
        }

        public async Task<PagedResult<Child>> GetChilds(int pageSize, int pageIndex, string query = null)
        {
            FilterDefinition<Child> filter = Builders<Child>.Filter.ElemMatch(c => c.FullName, query);
            var childs = await _ChildRepository.GetContext()
                                               .Result
                                               .Childs
                                               .Find(filter)
                                               .ToListAsync();

            var total = childs.Count;
            var pageResult = new PagedResult<Child>
            {
                List = childs,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };

            return pageResult;
        }

        public async Task<Child> GetChildByCpf(string cpf)
        {
            return await _ChildRepository.GetContext()
                                         .Result
                                         .Childs
                                         .Find(c => c.Cpf.Equals(cpf))
                                         .FirstOrDefaultAsync();

        }

        public async Task<Child> GetChildById(Guid id)
        {
            return await _ChildRepository.GetContext()
                                         .Result
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
