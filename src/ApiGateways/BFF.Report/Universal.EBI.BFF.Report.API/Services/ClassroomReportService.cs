using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Extensions;
using Universal.EBI.BFF.Report.API.Services.Interfaces;

namespace Universal.EBI.BFF.Report.API.Services
{
    public class ClassroomReportService : TextSerializerService, IClassroomReportService
    {
        private readonly HttpClient _httpClient;

        public ClassroomReportService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ReportUrl);
        }

        public async Task<FileContentResult> GetClassroomByDate(string inicialDate, string finalDate)
        {
            var response = await _httpClient.GetAsync($"/api/report/list-classroom-date?inicialDate={inicialDate}&finalDate={finalDate}");
            if (response == null) return null;
            //response.EnsureSuccessStatusCode();            
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            HandlerResponseErrors(response);                     
            
            var contentType = response.Content.Headers.ContentType.MediaType;
            var content = response.Content.ReadAsByteArrayAsync();
            var fileName = response.Content.Headers.ContentDisposition.FileName;
            var fileResult = new FileContentResult(content.Result, contentType);
            fileResult.FileDownloadName = fileName;

            return fileResult;

        }
    }
}
