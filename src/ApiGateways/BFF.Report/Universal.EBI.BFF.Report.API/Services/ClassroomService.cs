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
    public class ClassroomService : TextSerializerService, IClassroomService
    {
        private readonly HttpClient _httpClient;

        public ClassroomService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ClassrommUrl);
        }

        public async Task<ClassroomDto> GetClassroomById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/classroom/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<ClassroomDto>(response);
        }

        public async Task<PagedResult<ClassroomDto>> GetClassrooms(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/api/classrooms?ps={pageSize}&page={pageIndex}&q={query}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandlerResponseErrors(response);

            return await DeserializeResponseObject<PagedResult<ClassroomDto>>(response);
        }

        public async Task<ResponseResult> AddChildsClassroom(ClassroomDto classroom)
        {
            var classroomContent = GetContent(classroom);

            var response = await _httpClient.PostAsync("api/classroom/child/add", classroomContent);

            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<ResponseResult> CreateClassroom(ClassroomDto classroom)
        {
            var classroomContent = GetContent(classroom);

            var response = await _httpClient.PostAsync("/api/classroom/create", classroomContent);

            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }            

        public async Task<ResponseResult> UpdateClassroom(ClassroomDto classroom)
        {
            var classroomContent = GetContent(classroom);

            var response = await _httpClient.PutAsync("/api/Classroom/update", classroomContent);

            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<ResponseResult> DeleteClassroom(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/classroom/delete/{id}");
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }

        public async Task<ResponseResult> DeleteChildsClassroom(DeleteChildClassroomDto deleteChildClassroomDto)
        {
            var classroomContent = GetContent(deleteChildClassroomDto);
            var response = await _httpClient.PostAsync($"api/classroom/child/delete", classroomContent);
            if (!HandlerResponseErrors(response))
            {
                return await DeserializeResponseObject<ResponseResult>(response);
            }

            return ReturnOk();
        }
    }
}
