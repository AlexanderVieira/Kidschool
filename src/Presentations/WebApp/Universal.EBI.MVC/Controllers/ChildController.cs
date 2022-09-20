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
    public class ChildController : BaseController
    {
        private readonly IReportBffService _bffService;
        private readonly IAspNetUser _user;

        public ChildController(IReportBffService bffService, IAspNetUser user)
        {
            _bffService = bffService;
            _user = user;
        }

        [HttpGet]
        [Route("children")]
        public async Task<ActionResult> GetChildren([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            try
            {
                var response = await _bffService.GetChildren(ps, page, q);

                if (response.Value is not ResponseResult)
                {
                    ViewBag.Search = q;
                    var pagedResult = (PagedResult<ChildResponseDesignViewModel>)response.Value;
                    pagedResult.ReferenceAction = "Edit";
                    TempDataExtension.Put(TempData, "Children", pagedResult);
                    return View("Index", pagedResult);
                }

                if (HasResponseErrors((ResponseResult)response.Value)) TempData["Errors"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                               
                var rs = (ResponseResult)response.Value;
                if (rs.Status == 404)
                {
                    var pagedResult = new PagedResult<ChildResponseDesignViewModel> 
                    { 
                        List = new List<ChildResponseDesignViewModel>(), 
                        PageSize = ps, 
                        PageIndex = page, 
                        Query = q, 
                        ReferenceAction = "GetChildren", 
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
        [Route("children/inactives")]
        public async Task<ActionResult> GetChildrenInactives([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            try
            {
                var response = await _bffService.GetChildrenInactives(ps, page, q);

                if (response.Value is not ResponseResult)
                {
                    ViewBag.Search = q;
                    var pagedResult = (PagedResult<ChildResponseDesignViewModel>)response.Value;
                    pagedResult.ReferenceAction = "Edit";
                    TempDataExtension.Put(TempData, "Children", pagedResult);
                    return View("Index", pagedResult);
                }

                if (HasResponseErrors((ResponseResult)response.Value)) TempData["Errors"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

                var rs = (ResponseResult)response.Value;
                if (rs.Status == 404)
                {
                    var pagedResult = new PagedResult<ChildResponseDesignViewModel>
                    {
                        List = new List<ChildResponseDesignViewModel>(),
                        PageSize = ps,
                        PageIndex = page,
                        Query = q,
                        ReferenceAction = "GetChildrenInactives",
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
        [Route("child/details/{cpf:length(11)}")]
        public async Task<ActionResult> GetchildByCpf(string cpf)
        {            
            try
            {
                var response = await _bffService.GetChildByCpf(cpf);
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
                
        [HttpGet]
        [Route("child/details/{id:guid}")]
        public async Task<ActionResult> Details(Guid id)
        {            
            try
            {
                if (Guid.Empty == id)
                {
                    return View();
                }
                var response = await _bffService.GetChildById(id);
                if (response != null)
                {
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
                
        [HttpGet]
        [Route("child/create")]
        public ActionResult Create()
        {
            var vmChild = new ChildRequestViewModel();           
            return View(vmChild);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("child/create")]
        public async Task<ActionResult> Create(ChildRequestViewModel request)
        {
            if (!ModelState.IsValid) return View(request);
            try
            {
                request.Id = Guid.NewGuid();
                //request.FullName = $"{request.FirstName} {request.LastName}";
                //request.CreatedBy = _user.GetUserEmail();
                //request.CreatedDate = DateTime.Now;
                //request.Address.Id = Guid.NewGuid();
                //request.Phones.ToList().ForEach(c => c.Id = Guid.NewGuid());
                                
                request.Responsibles.ToList().ForEach(r => r.Id = Guid.NewGuid());
                //request.Responsibles.ToList().ForEach(r => r.FullName = $"{request.FirstName} {request.LastName}");
                //request.Responsibles.ToList().ForEach(r => r.CreatedDate = DateTime.Now);
                //request.Responsibles.ToList().ForEach(r => r.CreatedBy = _user.GetUserEmail());
                //request.Responsibles.ToList().ForEach(r => r.Address.Id = Guid.NewGuid());
                //request.Responsibles.ToList().ForEach(r => r.Phones.ToList().ForEach(p => p.Id = Guid.NewGuid()));                

                var response = await _bffService.CreateChild(request);
                if (HasResponseErrors(response)) 
                {
                    TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return View(request);
                }
                var vmChild = _bffService.GetChildById(request.Id);
                if (vmChild != null)
                {
                    return View("Details", vmChild);
                }
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
            catch(Exception ex)
            {
                var response = new ResponseResult();
                response.Errors.Messages.Add(ex.Message);
                if (HasResponseErrors(response)) TempData["Erros"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
        }
                
        [HttpGet]
        [Route("child/update/{id:guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid id)
        {
            try
            {
                if (Guid.Empty == id)
                {
                    return View();
                }
                var response = await _bffService.GetChildById(id);
                if (response != null)
                {
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
        [Route("child/update/{id:guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, ChildRequestViewModel request)
        {
            if (!ModelState.IsValid) return View(request);
            try
            {
                request.LastModifiedDate = DateTime.Now;
                request.LastModifiedBy = _user.GetUserEmail();                
                request.Responsibles.ToList().ForEach(r => r.LastModifiedDate = DateTime.Now);
                request.Responsibles.ToList().ForEach(r => r.LastModifiedBy = _user.GetUserEmail());

                var response = await _bffService.UpdateChild(request);
                if (HasResponseErrors(response))
                {
                    TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return View(request);
                }
                var vmChild = _bffService.GetChildById(id);
                if (vmChild != null)
                {
                    return View(nameof(Details), vmChild);
                }
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
            catch(Exception ex)
            {
                var response = new ResponseResult();
                response.Errors.Messages.Add(ex.Message);
                if (HasResponseErrors(response)) TempData["Erros"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
        }
                
        [HttpGet]
        [Route("child/delete/{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            if (Guid.Empty == id)
            {
                return RedirectToAction("Error", "Home", new { id = 404 });
            }
            var response = await _bffService.GetChildById(id);
            if (response != null)
            {
                return View(nameof(Details), response);
            }
            return RedirectToAction("Error", "Home", new { id = 404 });
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("child/delete/{id:guid}")]
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
                    return View(nameof(GetChildren));
                }                
                return RedirectToAction("Error", "Home", new { id = 404 });
            }
            catch(Exception ex)
            {
                var response = new ResponseResult();
                response.Errors.Messages.Add(ex.Message);
                if (HasResponseErrors(response)) TempData["Erros"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return RedirectToAction("Error", "Home", new { id = 500 });
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("child/ativate")]
        public ActionResult ActivateChild([FromBody] ChildViewModel vmChild)
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

        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("child/inativate")]
        public ActionResult InactivateChild([FromBody] ChildViewModel vmChild)
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

        [HttpGet]        
        [Route("child/add-responsible/{childId:guid}")]
        public ActionResult AddResponsible([FromRoute] Guid childId)
        {
            if (Guid.Empty == childId)
            {
                return View();
            }
            var request = new AddResponsibleRequestViewModel();                       
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("child/add-responsible/{childId:guid}")]
        public async Task<ActionResult> AddResponsible([FromRoute] Guid childId, AddResponsibleRequestViewModel request)
        {
            if (!ModelState.IsValid) return View(request);
            try
            {
                request.ResponsibleViewModel = new ResponsibleRequestViewModel
                {
                    CreatedBy = _user.GetUserEmail(),
                    CreatedDate = DateTime.Now
                };
                var response = await _bffService.AddResponsible(request);
                if (HasResponseErrors(response))
                {
                    TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return View(request);
                }
                return View(nameof(Details), new { id = request.ChildId });
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
    }
}
