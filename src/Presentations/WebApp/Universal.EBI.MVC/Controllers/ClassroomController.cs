﻿using Microsoft.AspNetCore.Http;
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
        public ActionResult Details(EducatorClassroomTransportViewModel vmClassroomEducator)
        {
            return View(vmClassroomEducator);
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
        public ActionResult Create(ClassroomViewModel vmClassroom)
        {
            var vmEducators = new List<EducatorViewModel>();
            ViewBag.Educators = vmEducators;
            var vmChildren = new List<ChildViewModel>();
            ViewBag.Children = vmChildren;
            var vmResponsibles = new List<ResponsibleViewModel>();
            ViewBag.Responsibles = vmResponsibles;

            try
            {
                vmClassroom.Id = Guid.NewGuid(); 
                vmClassroom.Educator.Id = Guid.NewGuid();
                var vmClassroomEducator = new EducatorClassroomTransportViewModel
                {
                    ClassroomId = Guid.NewGuid(),
                    Region = vmClassroom.Region,
                    Church = vmClassroom.Church,
                    Lunch = vmClassroom.Lunch,
                    CreatedDate = vmClassroom.CreatedDate,
                    Actived = vmClassroom.Actived,
                    ClassroomType = vmClassroom.ClassroomType,
                    MeetingTime = vmClassroom.MeetingTime,
                    CreatedBy = vmClassroom.CreatedBy,
                    LastModifiedDate = vmClassroom.LastModifiedDate,
                    LastModifiedBy = vmClassroom.LastModifiedBy,
                    EducatorId = vmClassroom.Educator.Id,
                    EducatorFirstName = vmClassroom.Educator.FirstName,
                    EducatorLastName = vmClassroom.Educator.LastName,
                    EducatorFunctionType = vmClassroom.Educator.FunctionType
                };

                return RedirectToAction(nameof(Details), vmClassroomEducator);
            }
            catch
            {
                return View(vmClassroom);
            }
        }

        // GET: ClassroomController/Edit/5
        public ActionResult Edit(ClassroomViewModel vmClassroom)
        {            
            var vmChildren = new List<ChildViewModel>();
            ViewBag.Children = vmChildren;
            var vmResponsibles = new List<ResponsibleViewModel>();
            ViewBag.Responsibles = vmResponsibles;
            var vmEducator = new EducatorViewModel { Id = Guid.NewGuid() };
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
