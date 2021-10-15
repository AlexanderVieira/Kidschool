using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Reports.API.Models;

namespace Universal.EBI.Reports.API.Services.Interfaces
{
    public interface IReportClassroom
    {
        public ICollection<Classroom> Classrooms { get; set; }
        Task<byte[]> GenerateReport(string renderFormat);
    }
}
