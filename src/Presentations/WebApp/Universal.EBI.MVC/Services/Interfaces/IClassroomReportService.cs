using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Universal.EBI.MVC.Services.Interfaces
{
    public interface IClassroomReportService
    {
        Task<FileContentResult> GetClassroomReportByDate(string initialDate, string finalDate, string region, string church);
        Task<FileContentResult> GetClassroomReportByYear(string region, string church);
        Task<FileContentResult> GetClassroomReportByMonth(string region, string church);
        Task<FileContentResult> GetClassroomReportByWeek(string region, string church);
        Task<FileContentResult> GetClassroomReportByDaily(string date, string region, string church);

    }
}
