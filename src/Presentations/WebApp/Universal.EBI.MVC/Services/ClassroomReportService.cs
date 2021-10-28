using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Universal.EBI.MVC.Extensions;
using Universal.EBI.MVC.Services.Interfaces;

namespace Universal.EBI.MVC.Services
{
    public class ClassroomReportService : TextSerializerService, IClassroomReportService
    {
        private readonly HttpClient _httpClient;

        public ClassroomReportService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ReportUrl);
        }

        public async Task<FileContentResult> GetClassroomReportByDaily(string date, string region, string church)
        {
            var response = await _httpClient.GetAsync($"/api/report/list-classroom-daily?date={date}&region={region}&church={church}");
            var fileResult = GetFileResult(response);
            return fileResult;
        }

        public async Task<FileContentResult> GetClassroomReportByDate(string initialDate, string finalDate, string region, string church)
        {
            var response = await _httpClient.GetAsync($"/api/report/list-classroom-date?initialDate" +
                                                      $"={initialDate}&finalDate={finalDate}&region={region}&church={church}");                      
            return GetFileResult(response);
        }        

        public async Task<FileContentResult> GetClassroomReportByMonth(string region, string church)
        {
            var response = await _httpClient.GetAsync($"/api/report/list-classroom-monthly?region={region}&church={church}");
            var fileResult = GetFileResult(response);
            return fileResult;
        }

        public async Task<FileContentResult> GetClassroomReportByWeek(string region, string church)
        {
            var response = await _httpClient.GetAsync($"/api/report/list-classroom-weekly?region={region}&church={church}");
            var fileResult = GetFileResult(response);
            return fileResult;
        }

        public async Task<FileContentResult> GetClassroomReportByYear(string region, string church)
        {
            var response = await _httpClient.GetAsync($"/api/report/list-classroom-yearly?region={region}&church={church}");
            var fileResult = GetFileResult(response);
            return fileResult;
        }

        private FileContentResult GetFileResult(HttpResponseMessage response)
        {
            if (response == null) return null;            

            if (HandlerResponseErrors(response))
            {
                var contentType = response.Content.Headers.ContentType.MediaType;
                var content = response.Content.ReadAsByteArrayAsync();
                var fileName = response.Content.Headers.ContentDisposition.FileName;
                var fileResult = new FileContentResult(content.Result, contentType);
                fileResult.FileDownloadName = fileName;
                return fileResult;
            }

            return null;
        }


    }
}
