using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Universal.EBI.MVC.Extensions;
using Universal.EBI.MVC.Models;
using Universal.EBI.MVC.Services.Interfaces;

namespace Universal.EBI.MVC.Controllers
{
    public class ChildController : BaseController
    {
        private readonly IReportBffService _bffService;

        public ChildController(IReportBffService bffService)
        {
            _bffService = bffService;
        }

        // GET: ChildController
        [HttpGet]
        [Route("children")]
        public async Task<IActionResult> GetChildren([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var pagedResultChildren = await _bffService.GetChildren(ps, page, q);
            ViewBag.Search = q;
            pagedResultChildren.ReferenceAction = "Edit";
            TempDataExtension.Put(TempData, "Children", pagedResultChildren);
            return RedirectToAction("Edit", "Classroom");
            //return View(children);
        }

        // GET: ChildController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: ChildController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChildController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChildViewModel vmChild)
        {
            try
            {
                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChildController/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: ChildController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ChildViewModel vmChild)
        {
            try
            {
                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChildController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChildController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ChildViewModel vmChild)
        {
            try
            {
                return RedirectToAction(nameof(GetChildren));
            }
            catch
            {
                return View();
            }
        }
    }
}
