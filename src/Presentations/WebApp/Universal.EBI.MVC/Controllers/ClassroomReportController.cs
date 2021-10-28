using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Universal.EBI.MVC.Services.Interfaces;

namespace Universal.EBI.MVC.Controllers
{
    public class ClassroomReportController : BaseController
    {
        private readonly IClassroomReportService _classroomReportService;

        public ClassroomReportController(IClassroomReportService classroomReportService)
        {
            _classroomReportService = classroomReportService;
        }

        [HttpGet]        
        [Route("report/list-classroom-date")]
        public async Task<IActionResult> GetClassroomReportByDate([FromQuery] string initialDate, 
                                                            [FromQuery] string finalDate, 
                                                            [FromQuery]  string region, 
                                                            [FromQuery]  string church)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _classroomReportService.GetClassroomReportByDate(initialDate, finalDate, region, church);            
            if (response == null) return BadRequest();
            return File(response.FileContents, response.ContentType, response.FileDownloadName);            

        }

        [HttpGet]        
        [Route("report/list-classroom-yearly")]
        public async Task<ActionResult> GetClassroomReportByYear([FromQuery] string region,
                                                           [FromQuery] string church)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _classroomReportService.GetClassroomReportByYear(region, church);
            if (response == null) return BadRequest();
            return File(response.FileContents, response.ContentType, response.FileDownloadName);

        }

        [HttpGet]        
        [Route("report/list-classroom-monthly")]
        public async Task<ActionResult> GetClassroomReportByMonth([FromQuery] string region,
                                                            [FromQuery] string church)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _classroomReportService.GetClassroomReportByYear(region, church);
            if (response == null) return BadRequest();
            return File(response.FileContents, response.ContentType, response.FileDownloadName);

        }

        [HttpGet]        
        [Route("report/list-classroom-weekly")]
        public async Task<ActionResult> GetClassroomReportByWeek([FromQuery] string region,
                                                           [FromQuery] string church)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _classroomReportService.GetClassroomReportByYear(region, church);
            if (response == null) return BadRequest();
            return File(response.FileContents, response.ContentType, response.FileDownloadName);

        }

        [HttpGet]        
        [Route("report/list-classroom-Daily")]
        public async Task<ActionResult> GetClassroomReportByDaily([FromQuery] string date,                                                            
                                                                  [FromQuery] string region,
                                                                  [FromQuery] string church)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _classroomReportService.GetClassroomReportByDaily(date, region, church);
            if (response == null) return BadRequest();
            return File(response.FileContents, response.ContentType, response.FileDownloadName);

        }
    }
}
