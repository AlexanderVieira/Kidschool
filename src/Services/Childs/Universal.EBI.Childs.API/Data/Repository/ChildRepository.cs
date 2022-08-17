using Universal.EBI.Childs.API.Models.Interfaces;
using System.Threading.Tasks;
using System;
using Universal.EBI.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Universal.EBI.Childs.API.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq;
using System.Collections.Generic;

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
            _context.Responsibles.UpdateRange(child.Responsibles);
            child.Responsibles = null;
            var childUpdated = _context.Children.Update(child);           
            return Task.FromResult(childUpdated.State.ToString() == "Modified");
        }

        public async Task<bool> DeleteChild(Child child)
        {
            var recoveredChild = await _context.Children                
                                     .Include(x => x.Address)
                                     .Include(x => x.Phones)
                                     .Include(x => x.Responsibles)                                     
                                     .ThenInclude(x => x.Phones)                                     
                                     .FirstOrDefaultAsync(c => c.Id == child.Id);
            
            var recoveredResponsibles = new List<Responsible>();
            foreach (var item in recoveredChild.Responsibles)
            {
                recoveredResponsibles = await _context.Responsibles
                                     .Include(x => x.Address)
                                     .Include(x => x.Phones)
                                     .Where(x => x.Id == item.Id).ToListAsync();
            }                  
                     
            _context.Responsibles.RemoveRange(recoveredResponsibles);

            recoveredResponsibles = await _context.Responsibles
                                     .Include(x => x.Address)
                                     .Include(x => x.Phones)
                                     .Where(c => c.Id == child.Id).ToListAsync();           

            var childRemoved = _context.Children.Remove(recoveredChild);

            return (recoveredResponsibles == null || recoveredResponsibles.Count == 0) && childRemoved.State.ToString().Equals("Deleted");
        }

        public Task<bool> ActivateChild(Child child)
        {            
            var childUpdated = _context.Children.Update(child);
            return Task.FromResult(childUpdated.State.ToString() == "Modified");
        }

        public Task<bool> InactivateChild(Child child)
        {            
            var childUpdated = _context.Children.Update(child);
            return Task.FromResult(childUpdated.State.ToString() == "Modified");
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
