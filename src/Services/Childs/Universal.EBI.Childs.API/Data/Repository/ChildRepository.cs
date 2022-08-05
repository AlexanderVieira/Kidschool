using Universal.EBI.Childs.API.Models.Interfaces;
using System.Threading.Tasks;
using System;
using Universal.EBI.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Universal.EBI.Childs.API.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

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
            return childCreated.Entity != null;
        }        

        public Task<bool> UpdateChild(Child child)
        {
            var childUpdated = _context.Children.Update(child);
            return Task.FromResult(childUpdated.Entity != null);
        }

        public async Task<bool> DeleteChild(Child child)
        {
            var childRecived = await _context.Children
                                      .Include(x => x.Address)
                                      .Include(x => x.Phones)
                                      .Include(x => x.Responsibles)
                                      .ThenInclude(x => x.Address)
                                      .FirstOrDefaultAsync(c => c.Id == child.Id);            
            var childRemoved = _context.Children.Remove(childRecived);
            return childRemoved.Entity == null;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }    

        public async Task<IDbContextTransaction> CriarTransacao()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<IDbContextTransaction> CriarTransacao(IsolationLevel isolation)
        {
            return await _context.Database.BeginTransactionAsync(isolation);
        }

        public Task<ChildDbContext> GetContext()
        {
            return Task.FromResult(_context);
        }
    }
}
