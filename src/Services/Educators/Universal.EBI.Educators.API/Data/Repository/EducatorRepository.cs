using Microsoft.EntityFrameworkCore;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Educators.API.Models;
using Universal.EBI.Educators.API.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Data.Common;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Educators.API.Data.Repository
{
    public class EducatorRepository : IEducatorRepository
    {
        private readonly EducatorContext _context;
        public IUnitOfWork UnitOfWork => _context;
        
        public EducatorRepository(EducatorContext context)
        {
            _context = context;
        }       

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Educator>> GetEducators()
        {
            return await _context.Educators.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Educator>> GetEducatorByName(string name)
        {
            return await _context.Educators.Where(e => e.FirstName == name).AsNoTracking().ToListAsync();
        }

        public async Task<Educator> GetEducatorByCpf(string cpf)
        {
            return await _context.Educators.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public async Task<Educator> GetEducatorById(Guid id)
        {
            return await _context.Educators.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task CreateEducator(Educator educator)
        {
            return Task.FromResult(_context.Educators.Add(educator));
        }

        public Task UpdateEducator(Educator educator)
        {
            return Task.FromResult(_context.Entry<Educator>(educator).State = EntityState.Modified);
        }

        public Task DeleteEducator(Guid id)
        {
            var educator = GetEducatorById(id);
            return Task.FromResult(_context.Remove(educator));            
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.EducatorId == id);
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
