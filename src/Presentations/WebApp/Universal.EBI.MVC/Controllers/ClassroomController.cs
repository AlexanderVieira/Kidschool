using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.MVC.Extensions;
using Universal.EBI.MVC.Models;
using Universal.EBI.MVC.Services.Interfaces;

namespace Universal.EBI.MVC.Controllers
{
    public class ClassroomController : BaseController
    {
        private readonly IReportBffService _bffService;

        public ClassroomController(IReportBffService bffService)
        {
            _bffService = bffService;
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        //[Route("Classroom/Details/{id}")]
        public ActionResult Details(Guid id)
        {
            //Recuparar classroom por Id
            var vmEducatorClassroom = new EducatorClassroomTransportViewModel();
            vmEducatorClassroom.ClassroomId = id;
            return View(vmEducatorClassroom);
        }


        [HttpGet]
        [Route("classroom/create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("classroom/create")]
        public async Task<IActionResult> Create(ClassroomViewModel vmClassroom)
        {
            try
            {
                vmClassroom.Id = Guid.NewGuid();                
                vmClassroom.CreatedDate = DateTime.Now.ToString();
                vmClassroom.CreatedBy = "CurrentUser";

                if (ModelState.IsValid) return View(vmClassroom);

                var response = await _bffService.CreateClassroom(vmClassroom);
                if (HasResponseErrors(response)) return View(vmClassroom);
                return RedirectToAction(nameof(Details), new { id = vmClassroom.Id });
            }
            catch
            {
                return View(vmClassroom);
            }
        }

        [HttpGet]
        //[Route("Classroom/Edit/{id}")]
        public ActionResult Edit(Guid id)
        {
            var vmClassroom = new ClassroomViewModel();
            var vmChildren = new List<ChildViewModel>();
            ViewBag.Children = vmChildren;
            var vmResponsibles = new List<ResponsibleViewModel>();
            ViewBag.Responsibles = vmResponsibles;
            var vmEducator = new EducatorClassroomViewModel { Id = Guid.NewGuid() };
            vmClassroom.Id = id;
            //vmClassroom.Region = vmEducatorClassroom.Region;
            //vmClassroom.Church = vmEducatorClassroom.Church;
            //vmClassroom.Lunch = vmEducatorClassroom.Lunch;
            //vmClassroom.CreatedDate = vmEducatorClassroom.CreatedDate;
            //vmClassroom.ClassroomType = vmEducatorClassroom.ClassroomType;
            //vmClassroom.Actived = vmEducatorClassroom.Actived;            
            //vmClassroom.MeetingTime = vmEducatorClassroom.MeetingTime;
            vmClassroom.Childs = new List<ChildViewModel>();
            vmClassroom.Childs.Add(new ChildViewModel { Id = Guid.NewGuid() });
            //vmClassroom.Childs[0].Responsibles = new List<ResponsibleViewModel>();
            //vmClassroom.Childs[0].Responsibles.Add(new ResponsibleViewModel { Id = Guid.NewGuid() });
            vmClassroom.Educator = vmEducator;

            return View(vmClassroom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ClassroomViewModel vmClassroom)
        {
            try
            {
                return RedirectToAction(nameof(Details), vmClassroom.Id);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Route("classroom/educadores")]
        public async Task<IActionResult> GetEducators([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var pagedResultEducator = await _bffService.GetEducators(ps, page, q);
            //TempData["Search"] = q;
            //pagedResultEducator.ReferenceAction = "Create";
            //TempData["Educators"] = pagedResultEducator;
            TempDataExtension.Put(TempData, "Educators", pagedResultEducator);
            return RedirectToAction("Create", "Classroom");
        }

        [HttpGet]
        [Route("classroom/events")]
        public IActionResult GetCalendarEvents()
        {
            //List<EventScheduleViewModel> events = _bffService.GetCalendarEvents(start, end);
            List<EventScheduleViewModel> events = new List<EventScheduleViewModel>();
            events.Add(
                new EventScheduleViewModel
                {
                    //Id = Guid.NewGuid(),
                    Title = "Reunião",
                    Description = "Dia do Reencontro com Deus",
                    Start = "2022-05-01 07:00",
                    End = "2022-05-01 07:00",
                    AllDay = false
                });
            return Json(events);
        }

        [HttpPost]        
        [Route("classroom/update-event")]
        public JsonResult UpdateEvent([FromBody]EventScheduleViewModel vmEvent)
        {
            string message = string.Empty;

            //message = _bffService.UpdateEvent(evt);

            return Json(new { message });
        }

        [HttpPost]        
        [Route("classroom/add-event")]
        public JsonResult AddEvent([FromBody] EventScheduleViewModel vmEvent)
        {
            string message = string.Empty;
            var eventId = Guid.NewGuid();

            //message = _bffService.AddEvent(evt, out eventId);

            return Json(new { message, eventId });
        }

        [HttpPost]        
        [Route("classroom/delete-event")]
        public JsonResult DeleteEvent([FromBody] EventScheduleViewModel vmEvent)
        {
            string message = string.Empty;

            //message = _bffService.DeleteEvent(vmEvent.Id);

            return Json(new { message });
        }
    }
}
