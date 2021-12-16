using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Extensions;
using Universal.EBI.BFF.Report.API.Models;
using Universal.EBI.BFF.Report.API.Services.Interfaces;

namespace Universal.EBI.BFF.Report.API.Services
{
    public class EducatorService : TextSerializerService, IEducatorService
    {
        private readonly HttpClient _httpClient;

        public EducatorService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5301");
        }
        public async Task<EducatorDto> GetEducatorByCpf(string cpf)
        {
            var response = await _httpClient.GetAsync($"/api/educator/{cpf}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<EducatorDto>(response);
        }

        public async Task<EducatorDto> GetEducatorById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/educator/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<EducatorDto>(response);
        }

        public async Task<PagedResult<EducatorClassroomDto>> GetEducators(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/educators?ps={pageSize}&page={pageIndex}&q={query}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<PagedResult<EducatorClassroomDto>>(response);
        }
    }
}
