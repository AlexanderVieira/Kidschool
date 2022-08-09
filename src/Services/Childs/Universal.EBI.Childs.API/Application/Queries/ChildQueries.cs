using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;


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
            var filter = new BsonDocument { { "FirstName", new BsonDocument { { "$regex", query }, { "$options", "i" } } } };

            var children = await _context.Children.Find(filter).ToListAsync();
            //var result = await _context.Children.Find(filter).ToListAsync();
            //var children = result.Select(c => new Child { FullName = c.FullName, GenderType = c.GenderType, BirthDate = c.BirthDate })
            //                     .OrderBy(c => c.FirstName).ToList();

            var total = children.Count;
            var pageResult = new PagedResult<Child>
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
            var children = await _context
                            .Children
                            .AsQueryable().ToListAsync();
            //.Find(c => c.Cpf.Number.Equals(cpf))

            var child = children.Where(c => c.Cpf.Number.Equals(cpf))
                            .Select(c => new Child { FirstName = c.FirstName, GenderType = c.GenderType, BirthDate = c.BirthDate })
                            .OrderBy(c => c.FirstName)
                            .SingleOrDefault();

            return child;
        }

        public async Task<Child> GetChildById(Guid id)
        {
            var child = await _context
                                    .Children //.AsQueryable().ToListAsync();
                                    .Find(c => c.Id == id).FirstOrDefaultAsync();
            //.ToListAsync();
            //.FirstOrDefaultAsync();
            //var child = children.Where(c => c.Id == id)
            //                    .Select(c => new Child { FirstName = c.FirstName, GenderType = c.GenderType, BirthDate = c.BirthDate })
            //                    .OrderBy(c => c.FirstName)
            //                    .SingleOrDefault();         

            return child;
        }

        public async Task<IEnumerable<Child>> GetChildsByName(string name)
        {
            var result = await _context.Children.Find(c => c.FirstName == name).ToListAsync();            
            var children = result.Select(c => new Child { FirstName = c.FirstName, GenderType = c.GenderType, BirthDate = c.BirthDate })
                                 .OrderBy(c => c.FirstName).ToList();           

            return children;
        }
    }

}
