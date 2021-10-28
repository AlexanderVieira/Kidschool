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
    public class ResponsibleRepository : IResponsibleRepository, IAggregateRoot
    {
        private readonly ReportContext _context;
        public IUnitOfWork UnitOfWork => _context;
        
        public ResponsibleRepository(ReportContext context)
        {
            _context = context;
        }       

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Responsible>> GetResponsibles()
        {
            return await _context.Responsibles.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Responsible>> GetResponsiblesByName(string name)
        {
            return await _context.Responsibles.Where(e => e.FirstName == name).AsNoTracking().ToListAsync();
        }

        public async Task<Responsible> GetResponsibleByCpf(string cpf)
        {
            return await _context.Responsibles.FirstOrDefaultAsync(c => c.Cpf.Equals(cpf));
        }

        public async Task<Responsible> GetResponsibleById(Guid id)
        {
            return await _context.Responsibles.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateResponsible(Responsible Responsible)
        {
            await _context.Responsibles.AddAsync(Responsible);
        }

        public Task UpdateResponsible(Responsible Responsible)
        {
            return Task.FromResult(_context.Entry<Responsible>(Responsible).State = EntityState.Modified);
        }

        public Task DeleteResponsible(Guid id)
        {
            var Responsible = GetResponsibleById(id);
            return Task.FromResult(_context.Remove(Responsible));            
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.ResponsibleId == id);
        }

        public async Task CreateAddress(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public DbConnection GetConnection()
        {
            return _context.Database.GetDbConnection();
        }
    }
}
