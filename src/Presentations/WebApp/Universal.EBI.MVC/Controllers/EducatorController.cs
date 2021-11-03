using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Universal.EBI.MVC.Services.Interfaces;

namespace Universal.EBI.MVC.Controllers
{
    public class EducatorController : Controller
    {
        private readonly IReportBffService _bffService;

        public EducatorController(IReportBffService bffService)
        {
            _bffService = bffService;
        }

        // GET: EducatorController
        [HttpGet]
        [Route("educadores")]
        public async Task<IActionResult> GetEducators([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var educators = await _bffService.GetEducators(ps, page, q);
            ViewBag.Search = q;
            educators.ReferenceAction = "Edit";
            return View(educators);
        }

        [HttpGet]
        [Route("educador/detalhe/{cpf:length(11)}")]
        public async Task<IActionResult> GetEducatorByCpf(string cpf)
        {
            var educator = await _bffService.GetEducatorByCpf(cpf);
            ViewBag.Cpf = cpf;
            return View(educator);
        }        

        // GET: EducatorController/Details/5
        [HttpGet]
        [Route("educador/detalhe/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var educator = await _bffService.GetEducatorById(id);
            ViewBag.Id = id;
            return View(educator);
        }

        // GET: EducatorController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EducatorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: EducatorController/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: EducatorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
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

        // GET: EducatorController/Delete/5
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: EducatorController/Delete/5
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
    }
}
