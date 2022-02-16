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

        // GET: ClassroomController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ClassroomController/Details/5
        [HttpGet]
        //[Route("Classroom/Details/{id}")]
        public ActionResult Details(Guid id)
        {
            //Recuparar classroom por Id
            var vmEducatorClassroom = new EducatorClassroomTransportViewModel();
            vmEducatorClassroom.ClassroomId = id;
            return View(vmEducatorClassroom);
        }

        // GET: ClassroomController/Create
        [HttpGet]
        [Route("Classroom/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassroomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Classroom/Create")]
        public async Task<IActionResult> Create(ClassroomViewModel vmClassroom)
        {            
            try
            {
                vmClassroom.Id = Guid.NewGuid();
                //vmClassroom.CreatedDate = $"{vmClassroom.CreatedDate} {DateTime.Now.ToShortTimeString()}";
                vmClassroom.CreatedDate = DateTime.Now.ToString();
                vmClassroom.CreatedBy = "CurrentUser";

                if (!ModelState.IsValid) return View(vmClassroom);
                
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
            vmClassroom.Childs[0].Responsibles = new List<ResponsibleViewModel>();
            vmClassroom.Childs[0].Responsibles.Add(new ResponsibleViewModel { Id = Guid.NewGuid() });
            vmClassroom.Educator = vmEducator;

            return View(vmClassroom);
        }

        // POST: ClassroomController/Edit/5
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

        // GET: ClassroomController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: ClassroomController/Delete/5
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
        [Route("Classroom/Educadores")]
        public async Task<IActionResult> GetEducators([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var pagedResultEducator = await _bffService.GetEducators(ps, page, q);
            //TempData["Search"] = q;
            //pagedResultEducator.ReferenceAction = "Create";
            //TempData["Educators"] = pagedResultEducator;
            TempDataExtension.Put(TempData, "Educators", pagedResultEducator);
            return RedirectToAction("Create","Classroom");
        }
    }
}
