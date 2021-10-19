using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Universal.EBI.Reports.API.Models;
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

            //var ClassroomList = _reportContext.Classrooms.Where(c => c.MeetingTime.Date >= InicialDate && c.MeetingTime.Date <= FinalDate).ToList();
            //var tipoSala = ClassroomList[0].ClassroomType.ToString();
            //var str = tipoSala.ToString();
            //var horario = ClassroomList[0].MeetingTime.ToString("HH:mm");
            //var data = ClassroomList[0].MeetingTime.ToShortDateString();
            //var shortDate = ClassroomList[0].MeetingTime.ToString("dd/MM/yyyy");            
            //ClassroomList[0].MeetingTime = Convert.ToDateTime(shortDate);
            //var ChildList = _reportContext.Children.ToList();           
            //var ResponsibleList = _reportContext.Responsibles.ToList();

            var webReport = HelperFastReport.WebReport("ClassroomsByDate.frx");
            var ClassroomList = _classroomRepository.GetClassroomByDate(inicialDate, finalDate).Result.ToList();
            var ChildrenList = _childrenRepository.GetChildren().Result.ToList();
            var ResponsibleList = _responsibleRepository.GetResponsibles().Result.ToList();

            var classrooms = HelperFastReport.GetDataTable<Classroom>(ClassroomList, "Classrooms");
            var children = HelperFastReport.GetDataTable<Child>(ChildrenList, "Children");
            var responsibles = HelperFastReport.GetDataTable<Responsible>(ResponsibleList, "Responsibles");

            webReport.Report.RegisterData(classrooms, "Classrooms");
            webReport.Report.RegisterData(children, "Children");
            webReport.Report.RegisterData(responsibles, "Responsibles");

            return File(HelperFastReport.ExportarPdf(webReport), "application/pdf", "SalasPorData.pdf");
            
        }
    }
}
