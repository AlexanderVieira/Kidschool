using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
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

        public async Task<ActionResult> ActivateChild(ChildRequestDto request)
        {
            var content = GetContent(request);
            var response = await _httpClient.PostAsync("/api/child/activate", content);
            return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
        }

        public async Task<ActionResult> AddResponsible(AddResponsibleRequestDto request)
        {
            var content = GetContent(request);
            var response = await _httpClient.PostAsync("/api/child/add/responsible", content);
            return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
        }

        public async Task<ActionResult> CreateChild(ChildRequestDto request)
        {
            var content = GetContent(request);
            var response = await _httpClient.PostAsync("/api/child/create", content);
            return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
        }

        public async Task<ActionResult> DeleteChild(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/child/delete/{id}");
            return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
        }

        public async Task<ActionResult> DeleteResponsible(DeleteResponsibleRequestDto request)
        {
            var content = GetContent(request);
            var response = await _httpClient.PutAsync("/api/child/delete/responsible", content);
            return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
        }

        public async Task<ActionResult> GetChildByCpf(string cpf)
        {
            var response = await _httpClient.GetAsync($"/api/child/{cpf}");
            if (!response.IsSuccessStatusCode) return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
            return new ObjectResult(await DeserializeResponseObject<ChildResponseDto>(response));
        }

        public async Task<ActionResult> GetChildById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/child/{id}");
            if (!response.IsSuccessStatusCode) return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
            return new ObjectResult(await DeserializeResponseObject<ChildResponseDto> (response));
        }

        public async Task<ActionResult> GetChildren(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/children?ps={pageSize}&page={pageIndex}&q={query}");
            if (!response.IsSuccessStatusCode)  
            {
                var objResultError = new ObjectResult(await DeserializeResponseObject<ResponseResult>(response))
                {
                    StatusCode = (int)response.StatusCode
                };
                return objResultError; 
            }

            var objResult = new ObjectResult(await DeserializeResponseObject<PagedResult<ChildDesignedQueryResponseDto>>(response))
            {
                StatusCode = (int)response.StatusCode
            };
            return objResult;
        }

        public async Task<ActionResult> GetChildrenInactives(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/children-inactives?ps={pageSize}&page={pageIndex}&q={query}");
            if (!response.IsSuccessStatusCode) return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
            return new ObjectResult(await DeserializeResponseObject<PagedResult<ChildDesignedQueryResponseDto>>(response));
        }

        public async Task<ActionResult> InactivateChild(ChildRequestDto request)
        {
            var content = GetContent(request);
            var response = await _httpClient.PostAsync("/api/child/inactivate", content);
            return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
        }

        public async Task<ActionResult> UpdateChild(ChildRequestDto request)
        {
            var content = GetContent(request);
            var response = await _httpClient.PutAsync("/api/child/update", content);
            return new ObjectResult(await DeserializeResponseObject<ResponseResult>(response));
        }
    }
}
