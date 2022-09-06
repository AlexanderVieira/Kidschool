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
        Task<ActionResult> GetChildrenInactives(int pageSize, int pageIndex, string query = null);
        Task<ActionResult> GetChildByCpf(string cpf);
        Task<ActionResult> GetChildById(Guid id);
        Task<ActionResult> CreateChild(ChildRequestDto request);
        Task<ActionResult> UpdateChild(ChildRequestDto request);
        Task<ActionResult> DeleteChild(Guid id);
        Task<ActionResult> ActivateChild(ChildRequestDto request);
        Task<ActionResult> InactivateChild(ChildRequestDto request);
        Task<ActionResult> AddResponsible(AddResponsibleRequestDto request);
        Task<ActionResult> DeleteResponsible(DeleteResponsibleRequestDto request);
    }
}
