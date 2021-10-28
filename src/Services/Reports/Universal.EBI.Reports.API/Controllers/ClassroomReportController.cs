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
    public class ClassroomReportController : BaseController
    {        
        private readonly IClassroomRepository _classroomRepository;
        private readonly IChildrenRepository _childrenRepository;
        private readonly IResponsibleRepository _responsibleRepository;

        public ClassroomReportController(IClassroomRepository classroomRepository, 
                                         IChildrenRepository childrenRepository, 
                                         IResponsibleRepository responsibleRepository)
        {
            _classroomRepository = classroomRepository;
            _childrenRepository = childrenRepository;
            _responsibleRepository = responsibleRepository;
        }
                
        [HttpGet("api/report/list-classroom-date")]
        public ActionResult GetClassroomByDate([FromQuery][Required] string initialDate, 
                                               [FromQuery][Required] string finalDate, 
                                               [FromQuery][Required] string region,
                                               [FromQuery][Required] string church)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var classrooms = _classroomRepository.GetClassroomByDate(DateTime.Parse(initialDate).Date, 
                                                                     DateTime.Parse(finalDate).Date, 
                                                                     region, church).Result.ToList();
            if (classrooms.Count <= 0)
            {
                AddProcessingErrors("Relatório não encontrado.");
                return CustomResponse();
            }

            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", $"{Guid.NewGuid()}.pdf");          
           
        }
                                               
        [HttpGet("api/report/list-classroom-yearly")]
        public ActionResult GetClassroomByYear([FromQuery][Required] string region,
                                               [FromQuery][Required] string church)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            
            var dias = -365.0;
            var initialDate = DateTime.Now.AddDays(dias).Date;
            var finalDate = DateTime.Now.Date;            
            var classrooms = _classroomRepository.GetClassroomByRange(initialDate, finalDate, region, church).Result.ToList();
            if (classrooms.Count <= 0)
            {
                AddProcessingErrors("Relatório não encontrado.");
                return CustomResponse();
            }            
            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", $"{Guid.NewGuid()}.pdf");

        }
                
        [HttpGet("api/report/list-classroom-monthly")]
        public ActionResult GetClassroomByMonth([FromQuery][Required] string region,
                                                [FromQuery][Required] string church)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var dias = -30.0;
            var initialDate = DateTime.Now.AddDays(dias);
            var finalDate = DateTime.Now;
            var classrooms = _classroomRepository.GetClassroomByRange(initialDate, finalDate, region, church).Result.ToList();
            if (classrooms.Count <= 0)
            {
                AddProcessingErrors("Relatório não encontrado.");
                return CustomResponse();
            }
            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", $"{Guid.NewGuid()}.pdf");

        }
                
        [HttpGet("api/report/list-classroom-weekly")]
        public ActionResult GetClassroomByWeek([FromQuery][Required] string region, 
                                               [FromQuery][Required] string church)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var dias = -7.0;
            var initialDate = DateTime.Now.AddDays(dias);
            var finalDate = DateTime.Now;
            var classrooms = _classroomRepository.GetClassroomByRange(initialDate, finalDate, region, church).Result.ToList();
            if (classrooms.Count <= 0 )
            {
                AddProcessingErrors("Relatório não encontrado.");
                return CustomResponse();
            }
            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", $"{Guid.NewGuid()}.pdf");

        }
                
        [HttpGet("api/report/list-classroom-daily")]
        public ActionResult GetClassroomByDaily([FromQuery][Required] string date, 
                                                [FromQuery][Required] string region,
                                                [FromQuery][Required] string church)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var classrooms = _classroomRepository.GetClassroomByDaily(DateTime.Parse(date).Date, region, church).Result.ToList();
            if (classrooms.Count <= 0)
            {
                AddProcessingErrors("Relatório não encontrado.");
                return CustomResponse();
            }

            return File(HelperFastReport.ExportarPdf(GetWebReportByDate(classrooms)), "application/pdf", $"{Guid.NewGuid()}.pdf");

        } 
        
        private WebReport GetWebReportByDate(List<Classroom> classroomList)
        {
            var webReport = HelperFastReport.WebReport("ClassroomsByDate.frx");            
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
