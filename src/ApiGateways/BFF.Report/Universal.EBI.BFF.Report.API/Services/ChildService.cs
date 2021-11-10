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
    public class ChildService : TextSerializerService, IChildService
    {
        private readonly HttpClient _httpClient;

        public ChildService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ChildUrl);
        }
        public async Task<ChildDto> GetChildByCpf(string cpf)
        {
            var response = await _httpClient.GetAsync($"/api/child/{cpf}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ChildDto>(response);
        }

        public async Task<ChildDto> GetChildById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/child/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ChildDto>(response);
        }

        public async Task<PagedResult<ChildDto>> GetChildren(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/childs?ps={pageSize}&page={pageIndex}&q={query}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<PagedResult<ChildDto>>(response);
        }
    }
}
