using System.Threading.Tasks;
using System;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Reports.API.Models;
using Universal.EBI.Reports.API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Universal.EBI.Reports.API.Data.Repository
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly ReportContext _context;        

        public IUnitOfWork UnitOfWork => _context;

        public ClassroomRepository(ReportContext context)
        {
            _context = context;
        }

        public async Task CreateClassroom(Classroom classroom)
        {
            await _context.Classrooms.AddAsync(classroom);
        }

        public async Task UpdateClassroom(Classroom classroom)
        {
            await Task.FromResult(_context.Entry(classroom).State = EntityState.Modified);
        }

        public async Task DeleteClassroom(Guid id)
        {
            var classroom = GetClassroomById(id);
            await Task.FromResult(_context.Remove(classroom));
        }     
        
        public async Task<Classroom> GetClassroomById(Guid id)
        {
            return await _context.Classrooms.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Classroom>> GetClassroomByDate(DateTime inicialDate, DateTime finalDate)
        {
            return await _context.Classrooms.Where(c => c.CreatedAt.Value.Date >= inicialDate && c.CreatedAt.Value.Date <= finalDate).ToListAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
