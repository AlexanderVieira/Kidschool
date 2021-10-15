﻿ using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Reports.API.Models;
using Universal.EBI.Reports.API.Services.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.Reports.API.Controllers
{
    public class ReportController : BaseController
    {
        //private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IReportClassroom _reportClassroom;

        public ReportController(IReportClassroom reportClassroom)
        {
            _reportClassroom = reportClassroom;
        }

        [HttpGet("api/report/print")]
        public async Task<IActionResult> Print([FromQuery] int flag)
        {
            switch (flag)
            {
                case 0:

                    string renderFormat = "PDF";
                    string mimetype = "application/pdf";
                    string extension = "pdf";
                    _reportClassroom.Classrooms = SeedDataBase();
                    var pdf = await _reportClassroom.GenerateReport(renderFormat);
                    return File(pdf, mimetype, "report." + extension);

                case 1:
                    
                    string renderFormatExcel = "Excel";
                    string mimetypeExcel = "application/msexcel";
                    string extensionExcel = "xls";
                    _reportClassroom.Classrooms = SeedDataBase();
                    var xls = await _reportClassroom.GenerateReport(renderFormatExcel);
                    return File(xls, mimetypeExcel, "report." + extensionExcel);

                default:
                    return CustomResponse(NotFound());
            }
        }

        private List<Classroom> SeedDataBase()
        {
            var classrooms = new List<Classroom>
            {
                new Classroom
                {
                    Region = "SÃO JOÃO I",
                    Church = "SÃO MATEUS II",
                    MeetingTime = DateTime.UtcNow,
                    ClassroomType = "MISTA",
                    Lunch = "IOGURTE E BISCOITO",
                    Educator = new Educator
                    {
                        FirstName = "JOSILENE"
                    },
                    Childs = new List<Child>
                    {
                        new Child
                        {
                            Id = Guid.NewGuid(),
                            FullName = "JONATHAN DE SALES DA SILVA",
                            BirthDate = DateTime.UtcNow.AddYears(-9),
                            GenderType = "M",
                            StartTimeMeeting = DateTime.UtcNow,
                            EndTimeMeeting = DateTime.UtcNow.AddHours(2.0),
                            Responsibles = new List<Responsible>
                            {
                                new Responsible
                                {
                                    Id = Guid.NewGuid(),
                                    FullName = "ALEXANDER VIEIRA DA SILVA",
                                    Cpf = new Core.DomainObjects.Cpf("041.380.007-55"),
                                    KinshipType = "PAI",
                                    Phones = new List<Phone>
                                    {
                                        new Phone
                                        {
                                            Number = "(21) 96520-0293"
                                        }
                                    }

                                }
                            }
                            
                        }
                    }
                }
            };

            return classrooms;

        }
    }
}
