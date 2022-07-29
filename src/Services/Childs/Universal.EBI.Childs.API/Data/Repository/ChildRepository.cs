using Universal.EBI.Childs.API.Models.Interfaces;
using System.Threading.Tasks;
using System;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace Universal.EBI.Childs.API.Data.Repository
{
    public class ChildRepository : IChildRepository
    {
        private readonly ChildDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ChildRepository(ChildDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateChild(Child child)
        {
            var childCreated = await _context.AddAsync(child);
            return childCreated != null;
        }        

        public Task<bool> UpdateChild(Child child)
        {
            var childUpdated = _context.Children.Update(child);
            return Task.FromResult(childUpdated != null);
        }

        public async Task<bool> DeleteChild(Guid id)
        {
            var child = await _context.Children.FirstOrDefaultAsync(c => c.Id == id);
            var childRemoved = _context.Children.Remove(child);
            return childRemoved == null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
