using Microsoft.EntityFrameworkCore;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Reports.API.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Data.Common;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Reports.API.Data.Repository
{
    public class ChildrenRepository : IChildrenRepository, IAggregateRoot
    {
        private readonly ReportContext _context;
        public IUnitOfWork UnitOfWork => _context;
        
        public ChildrenRepository(ReportContext context)
        {
            _context = context;
        }       

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Child>> GetChildren()
        {
            return await _context.Children.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Child>> GetChildrenByName(string name)
        {
            return await _context.Children.Where(e => e.FirstName == name).AsNoTracking().ToListAsync();
        }

        public async Task<Child> GetChildByCpf(string cpf)
        {
            return await _context.Children.FirstOrDefaultAsync(c => c.Cpf.Equals(cpf));
        }

        public async Task<Child> GetChildById(Guid id)
        {
            return await _context.Children.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task CreateChild(Child Child)
        {
            return Task.FromResult(_context.Children.Add(Child));
        }

        public Task UpdateChild(Child Child)
        {
            return Task.FromResult(_context.Entry<Child>(Child).State = EntityState.Modified);
        }

        public Task DeleteChild(Guid id)
        {
            var Child = GetChildById(id);
            return Task.FromResult(_context.Remove(Child));            
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.ChildId == id);
        }

        public Task CreateAddress(Address address)
        {
            return Task.FromResult(_context.Addresses.AddAsync(address));
        }

        public DbConnection GetConnection()
        {
            return _context.Database.GetDbConnection();
        }
    }
}
