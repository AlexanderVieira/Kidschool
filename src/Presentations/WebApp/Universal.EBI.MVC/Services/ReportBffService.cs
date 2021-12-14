using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Universal.EBI.Core.Comunication;
using Universal.EBI.MVC.Extensions;
using Universal.EBI.MVC.Models;
using Universal.EBI.MVC.Services.Interfaces;

namespace Universal.EBI.MVC.Services
{
    public class ReportBffService : TextSerializerService, IReportBffService
    {
        private readonly HttpClient _httpClient;

        public ReportBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ReportBffUrl);
        }

        public async Task<ChildViewModel> GetChildByCpf(string cpf)
        {
            var response = await _httpClient.GetAsync($"/reports/child/{cpf}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ChildViewModel>(response);
        }

        public async Task<ChildViewModel> GetChildById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/reports/child/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ChildViewModel>(response);
        }

        public async Task<PagedResult<ChildViewModel>> GetChildren(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"reports/children?ps={pageSize}&page={pageIndex}&q={query}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<PagedResult<ChildViewModel>>(response);
        }

        public async Task<EducatorViewModel> GetEducatorByCpf(string cpf)
        {
            var response = await _httpClient.GetAsync($"/reports/educator/{cpf}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<EducatorViewModel>(response);
        }

        public async Task<EducatorViewModel> GetEducatorById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/reports/educator/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<EducatorViewModel>(response);
        }

        public async Task<PagedResult<EducatorClassroomViewModel>> GetEducators(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"reports/educators?ps={pageSize}&page={pageIndex}&q={query}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<PagedResult<EducatorClassroomViewModel>>(response);
        }

        public async Task<ResponseResult> CreateClassroom(ClassroomViewModel classroom)
        {
            var classroomContent = GetContent(classroom);

            var response = await _httpClient.PostAsync("reports/classroom/create", classroomContent);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }
    }
}
