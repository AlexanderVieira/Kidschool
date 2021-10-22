using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Reports.API.Models.Interfaces;
using Universal.EBI.Reports.API.Services;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.Reports.API.Controllers
{
    public class ReportClassroomController : BaseController
    {        
        private readonly IClassroomRepository _classroomRepository;
        private readonly IChildrenRepository _childrenRepository;
        private readonly IResponsibleRepository _responsibleRepository;

        public ReportClassroomController(IClassroomRepository classroomRepository, 
                                         IChildrenRepository childrenRepository, 
                                         IResponsibleRepository responsibleRepository)
        {
            _classroomRepository = classroomRepository;
            _childrenRepository = childrenRepository;
            _responsibleRepository = responsibleRepository;
        }

        //[HttpGet("api/report/list-classroom-date/{inicialDate}/{finalDate}")]
        [HttpGet("api/report/list-classroom-date")]
        public ActionResult GetClassroomByDate([FromQuery] string inicialDate, [FromQuery] string finalDate)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var classrooms = _classroomRepository.GetClassroomByDate(DateTime.Parse(inicialDate).Date, DateTime.Parse(finalDate).Date).Result.ToList();
            if (classrooms.Count <= 0)
            {
                AddProcessingErrors("Salas de aula não encontradas.");
                return CustomResponse();
            }

            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", $"{Guid.NewGuid()}.pdf");
            //var fileContent = new FileContentResult(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf");
            //fileContent.FileDownloadName = "SalasPorData.pdf";
            //return CustomResponse(fileContent);

        }

        [HttpGet("api/report/list-classroom-yearly/{inicialDate}/{finalDate}/{region}/{church}")]
        public ActionResult GetClassroomByYearly([FromRoute][Required] DateTime inicialDate,
                                                 [FromRoute][Required] DateTime finalDate,
                                                 [FromRoute][Required] string region,
                                                 [FromRoute][Required] string church)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var classrooms = _classroomRepository.GetClassroomByRange(inicialDate, finalDate, region, church).Result.ToList();
            if (classrooms.Count <= 0)
            {
                AddProcessingErrors("Salas de aula não encontradas.");
                return CustomResponse();
            }
            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", "SalasPorData.pdf");

        }

        [HttpGet("api/report/list-classroom-monthly/{inicialDate}/{finalDate}/{region}/{church}")]
        public ActionResult GetClassroomByMonthly([FromRoute][Required] DateTime inicialDate,
                                                 [FromRoute][Required] DateTime finalDate,
                                                 [FromRoute][Required] string region,
                                                 [FromRoute][Required] string church)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var classrooms = _classroomRepository.GetClassroomByRange(inicialDate, finalDate, region, church).Result.ToList();
            if (classrooms.Count <= 0)
            {
                AddProcessingErrors("Salas de aula não encontradas.");
                return CustomResponse();
            }
            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", "SalasPorData.pdf");

        }

        [HttpGet("api/report/list-classroom-weekly/{inicialDate}/{finalDate}/{region}/{church}")]
        public ActionResult GetClassroomByWeekly([FromRoute][Required] DateTime inicialDate, 
                                                 [FromRoute][Required] DateTime finalDate, 
                                                 [FromRoute][Required] string region, 
                                                 [FromRoute][Required] string church)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }            
            var classrooms = _classroomRepository.GetClassroomByRange(inicialDate, finalDate, region, church).Result.ToList();
            if (classrooms.Count <= 0 )
            {
                AddProcessingErrors("Salas de aula não encontradas.");
                return CustomResponse();
            }
            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", "SalasPorData.pdf");

        }

        [HttpGet("api/report/list-classroom-daily/{date}")]
        public ActionResult GetClassroomByDaily([FromRoute][Required] DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var classrooms = _classroomRepository.GetClassroomByDaily(date).Result.ToList();
            if (classrooms.Count <= 0)
            {
                AddProcessingErrors("Salas de aula não encontradas.");
                return CustomResponse();
            }

            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", "SalasPorData.pdf");

        } 
        
        private WebReport GetWebReportByDate(List<Classroom> classroomList)
        {
            var webReport = HelperFastReport.WebReport("ClassroomsByDate.frx");
            //classroomList = _classroomRepository.GetClassroomByRange(inicialDate, finalDate, region, church).Result.ToList();
            var childrenList = _childrenRepository.GetChildren().Result.ToList();
            var responsibleList = _responsibleRepository.GetResponsibles().Result.ToList();

            var classrooms = HelperFastReport.GetDataTable<Classroom>(classroomList, "Classrooms");
            var children = HelperFastReport.GetDataTable<Child>(childrenList, "Children");
            var responsibles = HelperFastReport.GetDataTable<Responsible>(responsibleList, "Responsibles");

            webReport.Report.RegisterData(classrooms, "Classrooms");
            webReport.Report.RegisterData(children, "Children");
            webReport.Report.RegisterData(responsibles, "Responsibles");

            return webReport;
        }
    }
}
