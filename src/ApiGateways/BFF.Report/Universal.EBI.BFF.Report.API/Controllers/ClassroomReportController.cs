using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Services.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.BFF.Report.API.Controllers
{
    public class ClassroomReportController : BaseController
    {
        private readonly IClassroomReportService _classroomReportService;

        public ClassroomReportController(IClassroomReportService classroomReportService)
        {
            _classroomReportService = classroomReportService;
        }

        [HttpGet]
        //[Route("reports/report/list-classroom-date/{inicialDate}/{finalDate}")]
        [Route("reports/report/list-classroom-date")]
        public async Task<ActionResult> GetClassroomByDate([FromQuery] string inicialDate, [FromQuery] string finalDate)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var response = await _classroomReportService.GetClassroomByDate(inicialDate, finalDate);

            if (response == null)
            {
                AddProcessingErrors("Salas de aula não encontradas.");
                return CustomResponse();
            }

            return File(response.FileContents, response.ContentType, response.FileDownloadName);
            //return CustomResponse(response);

        }
    }
}
