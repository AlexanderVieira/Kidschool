using System;
using System.Data;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Childs.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public async Task<Child> GetChildById(Guid id)
        {
            var recoveredChild = await _context.Children
                                     .Include(x => x.Address)
                                     .Include(x => x.Phones)                                     
                                     .Include(x => x.Responsibles)
                                     .ThenInclude(x => x.Phones)
                                     .Include(x => x.Responsibles)
                                     .ThenInclude(x => x.Address)
                                     .FirstOrDefaultAsync(c => c.Id == id); 

            return recoveredChild;
        }

        public async Task<bool> CreateChild(Child child)
        {
            var childCreated = await _context.AddAsync(child);
            return childCreated.Entity != null;
        }

        public Task<bool> UpdateChild(Child child)
        {            
            _context.Responsibles.UpdateRange(child.Responsibles);
            child.Responsibles = null;
            var childUpdated = _context.Children.Update(child);
            return Task.FromResult(childUpdated.State.ToString() == "Modified");            
        }

        public Task<bool> DeleteChild(Child child)
        {
            _context.Responsibles.RemoveRange(child.Responsibles);          
            var childRemoved = _context.Children.Remove(child);            
            return Task.FromResult(childRemoved.State.ToString().Equals("Deleted"));
        }

        public async Task<bool> ActivateChild(Child child)
        {            
            var updated = await UpdateChild(child);
            return updated;
        }

        public async Task<bool> InactivateChild(Child child)
        {
            var updated = await UpdateChild(child);
            return updated;
        }

        public Task<bool> AddResponsible(Child child)
        {
            _context.Responsibles.AddRange(child.Responsibles);
            var childUpdated = _context.Children.Update(child);
            return Task.FromResult(childUpdated.State.ToString() == "Modified");
        }

        public Task<bool> DeleteResponsible(Child child)
        {
            //var addressRemoved = _context.Addresses.Remove(child.Responsibles[0].Address);
            //_context.Phones.RemoveRange(child.Responsibles[0].Phones);            
            _context.Database.ExecuteSqlInterpolated($"DELETE FROM ChildResponsible WHERE (ResponsiblesId = {child.Responsibles[0].Id}) AND (ChildrenId = {child.Id})");
            var responsibleRemoved = _context.Responsibles.Remove(child.Responsibles[0]);
            var childUpdated = _context.Children.Update(child);
            return Task.FromResult(responsibleRemoved.State.ToString().Equals("Deleted"));            
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

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }
        
    }
}
