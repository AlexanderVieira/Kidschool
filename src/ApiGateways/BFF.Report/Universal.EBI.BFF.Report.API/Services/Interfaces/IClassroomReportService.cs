using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Universal.EBI.BFF.Report.API.Services.Interfaces
{
    public interface IClassroomReportService
    {
        Task<FileContentResult> GetClassroomByDate(string inicialDate, string finalDate);
    }
}
