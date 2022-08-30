using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Models;

namespace Universal.EBI.BFF.Report.API.Services.Interfaces
{
    public interface IChildService
    {
        Task<ActionResult> GetChildren(int pageSize, int pageIndex, string query = null);
        Task<ChildResponseDto> GetChildByCpf(string cpf);
        Task<ChildResponseDto> GetChildById(Guid id);
    }
}
