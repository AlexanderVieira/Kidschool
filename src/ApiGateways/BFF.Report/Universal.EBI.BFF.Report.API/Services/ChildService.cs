using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Extensions;
using Universal.EBI.BFF.Report.API.Models;
using Universal.EBI.BFF.Report.API.Services.Interfaces;
using Universal.EBI.Core.Comunication;

namespace Universal.EBI.BFF.Report.API.Services
{
    public class ChildService : TextSerializerService, IChildService
    {
        private readonly HttpClient _httpClient;

        public ChildService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5501");
        }
        public async Task<ChildResponseDto> GetChildByCpf(string cpf)
        {
            var response = await _httpClient.GetAsync($"/api/child/{cpf}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ChildResponseDto>(response);
        }

        public async Task<ChildResponseDto> GetChildById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/child/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ChildResponseDto>(response);
        }

        public async Task<ActionResult> GetChildren(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/children?ps={pageSize}&page={pageIndex}&q={query}");
            if (!response.IsSuccessStatusCode) return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));            
            
            return new ObjectResult(await DeserializeResponseObject<PagedResult<ChildDesignedQueryResponseDto>>(response));
        }
    }
}
