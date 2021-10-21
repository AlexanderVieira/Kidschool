using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("api/report/list-classroom-date/{inicialDate}/{finalDate}")]
        public ActionResult GetClassroomByDate([FromRoute][Required] DateTime inicialDate, [FromRoute][Required] DateTime finalDate)
        {
             if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var webReport = HelperFastReport.WebReport("ClassroomsByDate.frx");
            var classroomList = _classroomRepository.GetClassroomByDate(inicialDate, finalDate).Result.ToList();
            var childrenList = _childrenRepository.GetChildren().Result.ToList();
            var responsibleList = _responsibleRepository.GetResponsibles().Result.ToList();           

            var classrooms = HelperFastReport.GetDataTable<Classroom>(classroomList, "Classrooms");
            var children = HelperFastReport.GetDataTable<Child>(childrenList, "Children");
            var responsibles = HelperFastReport.GetDataTable<Responsible>(responsibleList, "Responsibles");                      

            webReport.Report.RegisterData(classrooms, "Classrooms");
            webReport.Report.RegisterData(children, "Children");
            webReport.Report.RegisterData(responsibles, "Responsibles");

            return File(HelperFastReport.ExportarPdf(webReport), "application/pdf", "SalasPorData.pdf");
            
        }
    }
}
