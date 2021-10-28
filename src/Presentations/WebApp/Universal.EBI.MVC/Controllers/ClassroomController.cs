using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Universal.EBI.MVC.Models;

namespace Universal.EBI.MVC.Controllers
{
    public class ClassroomController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Route("classroom-create")]
        public PartialViewResult ClassroomCreate() 
        {
            var vmEducators = new List<EducatorViewModel>();
            ViewBag.Educators = vmEducators;
            return PartialView("~/Views/Shared/Classroom/_ClassroomCreatePartial.cshtml");
        }

        [HttpGet]
        [Route("classroom-educators")]
        public PartialViewResult Educators()
        {
            return PartialView("~/Views/Shared/Educator/_EducatorCollectionPartial.cshtml");
        }
        //D:Desenv/Workspace/Projetos/VS2019/Universal.EBI/src/Presentations/WebApp/Universal.EBI.MVC/Views/Shared/Classroom/_ClassroomForm.cshtml
        //~/Views/Shared/Classroom/_ClassroomForm.cshtml
        [HttpGet]
        [Route("classroom/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClassroomViewModel vmClassroom)
        {
            return View(new ClassroomViewModel());
        }
       
    }
}
