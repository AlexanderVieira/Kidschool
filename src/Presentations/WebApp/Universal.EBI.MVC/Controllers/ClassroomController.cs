using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Core.Comunication;
using Universal.EBI.MVC.Extensions;
using Universal.EBI.MVC.Models;
using Universal.EBI.MVC.Services.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.MVC.Controllers
{
    public class ClassroomController : BaseController
    {
        private readonly IReportBffService _bffService;
        private readonly IAspNetUser _user;

        public ClassroomController(IReportBffService bffService, IAspNetUser user)
        {
            _bffService = bffService;
            _user = user;
        }

        [HttpGet]
        [Route("Classrooms")]
        public async Task<ActionResult> GetClassrooms([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            try
            {
                var response = await _bffService.GetClassrooms(ps, page, q);

                if (response.Value is not ResponseResult)
                {
                    ViewBag.Search = q;
                    var pagedResult = (PagedResult<ClassroomResponseViewModel>)response.Value;
                    pagedResult.ReferenceAction = "GetClassrooms";
                    TempDataExtension.Put(TempData, "Classrooms", pagedResult);
                    return View("Index", pagedResult);
                }

                if (HasResponseErrors((ResponseResult)response.Value)) TempData["Errors"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

                var rs = (ResponseResult)response.Value;
                if (rs.Status == 404)
                {
                    var pagedResult = new PagedResult<ClassroomResponseViewModel>
                    {
                        List = new List<ClassroomResponseViewModel>(),
                        PageSize = ps,
                        PageIndex = page,
                        Query = q,
                        ReferenceAction = "GetClassrooms",
                        TotalResults = ps * page
                    };
                    return View("Index", pagedResult);
                }
                return RedirectToAction("Error", "Home", new { id = rs.Status });

            }
            catch (Exception ex)
            {
                var response = new ResponseResult();
                response.Errors.Messages.Add(ex.Message);
                if (HasResponseErrors(response)) TempData["Erros"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
        }

        [HttpGet]
        [Route("Classroom/Details/{id}")]
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                if (Guid.Empty == id)
                {
                    return View();
                }
                var response = await _bffService.GetClassroomById(id);
                if (response != null)
                {
                    var vmTransport = new EducatorClassroomTransportViewModel()
                    {
                        ClassroomId = response.Id,
                        Region = response.Region,
                        Church = response.Church,
                        ClassroomType = response.ClassroomType,
                        Actived = response.Actived,
                        Lunch = response.Lunch,
                        MeetingTime = response.MeetingTime,
                        CreatedBy = response.CreatedBy,
                        CreatedDate = response.CreatedDate,
                        LastModifiedBy = response.LastModifiedBy,
                        LastModifiedDate = response.LastModifiedDate,
                        EducatorId = response.Educator.Id,
                        EducatorFirstName = response.Educator.FirstName,
                        EducatorLastName = response.Educator.LastName,
                        EducatorFunctionType = response.Educator.FunctionType
                    };

                    return View(vmTransport);
                }
                return RedirectToAction("Error", "Home", new { id = 404 });
            }
            catch (Exception ex)
            {
                var response = new ResponseResult();
                response.Errors.Messages.Add(ex.Message);
                if (HasResponseErrors(response)) TempData["Erros"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
           
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
            if (!ModelState.IsValid) return View(vmClassroom);
            try
            {
                vmClassroom.Id = Guid.NewGuid();                
                vmClassroom.CreatedDate = DateTime.Now;
                vmClassroom.CreatedBy = _user.GetUserEmail();                

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
        [Route("classroom/update/{id:guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid id)
        {
            try
            {
                if (Guid.Empty == id)
                {
                    return View();
                }
                var response = await _bffService.GetClassroomById(id);
                if (response != null)
                {
                    //response.Children = response.Children ?? new List<ChildClassroomViewModel>() { new ChildClassroomViewModel() };
                    if(response.Children == null) response.Children = new List<ChildClassroomViewModel>() { new ChildClassroomViewModel() };
                    if (!response.Children.Any()) response.Children = new List<ChildClassroomViewModel>() { new ChildClassroomViewModel() };
                    return View(response);
                }
                return RedirectToAction("Error", "Home", new { id = 404 });
            }
            catch (Exception ex)
            {
                var response = new ResponseResult();
                response.Errors.Messages.Add(ex.Message);
                if (HasResponseErrors(response)) TempData["Erros"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("classroom/update/{id:guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, ClassroomViewModel request)
        {
            if (!ModelState.IsValid) return View(request);
            try
            {
                request.LastModifiedDate = DateTime.Now;
                request.LastModifiedBy = _user.GetUserEmail();               

                var response = await _bffService.UpdateClassroom(request);
                if (HasResponseErrors(response))
                {
                    TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return View(request);
                }
                var vmChild = _bffService.GetClassroomById(id).Result;
                if (vmChild != null)
                {
                    //vmChild.Children = vmChild.Children ?? new List<ChildClassroomViewModel>() { new ChildClassroomViewModel() };
                    if (vmChild.Children == null) new List<ChildClassroomViewModel>() { new ChildClassroomViewModel() };
                    if (!vmChild.Children.Any()) new List<ChildClassroomViewModel>() { new ChildClassroomViewModel() };
                    return View(nameof(Details), vmChild);
                }
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
            catch (Exception ex)
            {
                var response = new ResponseResult();
                response.Errors.Messages.Add(ex.Message);
                if (HasResponseErrors(response)) TempData["Erros"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
        }

        [HttpGet]
        [Route("classroom/delete/{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            if (Guid.Empty == id)
            {
                return RedirectToAction("Error", "Home", new { id = 404 });
            }
            var response = await _bffService.GetClassroomById(id);
            if (response != null)
            {
                return View(nameof(Details), response);
            }
            return RedirectToAction("Error", "Home", new { id = 404 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("classroom/delete/{id:guid}")]
        public async Task<ActionResult> ConfirmDeletion(Guid id)
        {
            try
            {
                if (Guid.Empty == id)
                {
                    return RedirectToAction("Error", "Home", new { id = 404 });
                }
                var response = await _bffService.DeleteChild(id);
                if (HasResponseErrors(response))
                {
                    TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return View(nameof(Index));
                }
                return RedirectToAction("Error", "Home", new { id = 404 });
            }
            catch (Exception ex)
            {
                var response = new ResponseResult();
                response.Errors.Messages.Add(ex.Message);
                if (HasResponseErrors(response)) TempData["Erros"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
        }

        [HttpGet]
        [Route("classroom/educadores")]
        public async Task<IActionResult> GetEducators([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {            
            var response = await _bffService.GetEducators(ps, page, q);

            if (response != null)
            {
                ViewBag.Search = q;
                var pagedResult = response;
                pagedResult.ReferenceAction = "Create";
                TempDataExtension.Put(TempData, "Classrooms", pagedResult);
                return View("Create", pagedResult);
            }

            TempData["Errors"] =
                ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                        
            return RedirectToAction("Error", "Home", new { id = 404 });

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
