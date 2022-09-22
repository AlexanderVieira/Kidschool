using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        public async Task<ChildResponseViewModel> GetChildByCpf(string cpf)
        {
            var response = await _httpClient.GetAsync($"/api/bff/child/{cpf}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ChildResponseViewModel>(response);
        }

        public async Task<ChildResponseViewModel> GetChildById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/bff/child/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ChildResponseViewModel>(response);
        }

        public async Task<ObjectResult> GetChildren(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/bff/children?ps={pageSize}&page={pageIndex}&q={query}");

            if (!response.IsSuccessStatusCode)
            {
                var objResultError = new ObjectResult(await DeserializeResponseObject<ResponseResult>(response))
                {
                    StatusCode = (int)response.StatusCode
                };
                return objResultError;
            }

            var objResult = new ObjectResult(await DeserializeResponseObject<PagedResult<ChildResponseDesignViewModel>>(response))
            {
                StatusCode = (int)response.StatusCode
            };
            return objResult;                       
        }

        public async Task<ObjectResult> GetChildrenInactives(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/bff/children-inactives?ps={pageSize}&page={pageIndex}&q={query}");

            if (!response.IsSuccessStatusCode)
            {
                var objResultError = new ObjectResult(await DeserializeResponseObject<ResponseResult>(response))
                {
                    StatusCode = (int)response.StatusCode
                };
                return objResultError;
            }

            var objResult = new ObjectResult(await DeserializeResponseObject<PagedResult<ChildResponseDesignViewModel>>(response))
            {
                StatusCode = (int)response.StatusCode
            };
            return objResult;
        }

        public async Task<ResponseResult> CreateChild(ChildRequestViewModel request)
        {
            var content = GetContent(request);

            var response = await _httpClient.PostAsync("/api/bff/child/create", content);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateChild(ChildRequestViewModel request)
        {
            var content = GetContent(request);

            var response = await _httpClient.PutAsync("/api/bff/child/update", content);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> InactivateChild(ChildRequestViewModel request)
        {
            var content = GetContent(request);

            var response = await _httpClient.PutAsync("/api/bff/child/inactivate", content);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> ActivateChild(ChildRequestViewModel request)
        {
            var content = GetContent(request);

            var response = await _httpClient.PutAsync("/api/bff/child/activate", content);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> DeleteChild(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/bff/child/delete/{id}");
            
            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> AddResponsible(AddResponsibleRequestViewModel request)
        {
            var content = GetContent(request);

            var response = await _httpClient.PostAsync("/api/bff/child/add/responsible", content);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> DeleteResponsible(DeleteResponsibleRequestViewModel request)
        {
            var content = GetContent(request);

            var response = await _httpClient.PutAsync("/api/bff/child/delete/responsible", content);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<EducatorViewModel> GetEducatorByCpf(string cpf)
        {
            var response = await _httpClient.GetAsync($"/api/bff/educator/{cpf}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<EducatorViewModel>(response);
        }

        public async Task<EducatorViewModel> GetEducatorById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/bff/educator/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<EducatorViewModel>(response);
        }

        public async Task<PagedResult<EducatorClassroomViewModel>> GetEducators(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/bff/educators?ps={pageSize}&page={pageIndex}&q={query}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<PagedResult<EducatorClassroomViewModel>>(response);
        }        

        public async Task<ClassroomViewModel> GetClassroomById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/bff/classroom/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ClassroomViewModel>(response);
        }

        public async Task<ObjectResult> GetClassrooms(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/bff/classrooms?ps={pageSize}&page={pageIndex}&q={query}");

            if (!response.IsSuccessStatusCode)
            {
                var objResultError = new ObjectResult(await DeserializeResponseObject<ResponseResult>(response))
                {
                    StatusCode = (int)response.StatusCode
                };
                return objResultError;
            }

            var objResult = new ObjectResult(await DeserializeResponseObject<PagedResult<ClassroomResponseViewModel>>(response))
            {
                StatusCode = (int)response.StatusCode
            };
            return objResult;
        }

        public async Task<ResponseResult> CreateClassroom(ClassroomViewModel classroom)
        {
            var classroomContent = GetContent(classroom);

            var response = await _httpClient.PostAsync("/api/bff/classroom/create", classroomContent);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateClassroom(ClassroomViewModel request)
        {
            var content = GetContent(request);

            var response = await _httpClient.PutAsync("/api/bff/classroom/update", content);

            if (!HandlerResponseErrors(response)) return await DeserializeResponseObject<ResponseResult>(response);

            return ReturnOk();
        }

    }
}
