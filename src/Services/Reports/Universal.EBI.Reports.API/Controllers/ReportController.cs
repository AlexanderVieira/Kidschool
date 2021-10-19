 using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Core.DomainObjects;
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
            //var classrooms = new List<Classroom>
            //{
            //    new Classroom
            //    {
            //        Region = "SÃO JOÃO I",
            //        Church = "SÃO MATEUS II",
            //        MeetingTime = DateTime.UtcNow,
            //        ClassroomType = "MISTA",
            //        Lunch = "IOGURTE E BISCOITO",
            //        Educator = new Educator
            //        {
            //            FirstName = "JOSILENE"
            //        },
            //        Childs = new List<Child>
            //        {
            //            new Child
            //            {
            //                Id = 1, //Guid.NewGuid(),
            //                FullName = "JONATHAN DE SALES DA SILVA",
            //                BirthDate = DateTime.UtcNow.AddYears(-9),
            //                GenderType = "M",
            //                StartTimeMeeting = DateTime.UtcNow,
            //                EndTimeMeeting = DateTime.UtcNow.AddHours(2.0),
            //                Responsibles = new List<Responsible>
            //                {
            //                    new Responsible
            //                    {
            //                        Id = 1, //Guid.NewGuid(),
            //                        FullName = "ALEXANDER VIEIRA DA SILVA",
            //                        Cpf = new Cpf("709.375.217-95"),
            //                        KinshipType = "PAI",
            //                        Phones = new List<Phone>
            //                        {
            //                            new Phone
            //                            {
            //                                Number = "(21) 98469-0691"
            //                            }
            //                        }

            //                    }
            //                }
                            
            //            },
            //            new Child
            //            {
            //                Id = 2, //Guid.NewGuid(),
            //                FullName = "GUILHERME GONÇALVE DIAS",
            //                BirthDate = DateTime.UtcNow.AddYears(-11),
            //                GenderType = "M",
            //                StartTimeMeeting = DateTime.UtcNow.AddDays(-1.0),
            //                EndTimeMeeting = DateTime.UtcNow.AddDays(-1.0).AddHours(2.0),
            //                Responsibles = new List<Responsible>
            //                {
            //                    new Responsible
            //                    {
            //                        Id = 2, //Guid.NewGuid(),
            //                        FullName = "RODRIGO GONÇALVES DIAS",
            //                        Cpf = new Cpf("439.599.527-67"),
            //                        KinshipType = "PAI",
            //                        Phones = new List<Phone>
            //                        {
            //                            new Phone
            //                            {
            //                                Number = "(21) 95598-0668"
            //                            }
            //                        }

            //                    }
            //                }

            //            }
            //        }
            //    },
            //    new Classroom
            //    {
            //        Region = "SÃO JOÃO I",
            //        Church = "SÃO MATEUS II",
            //        MeetingTime = DateTime.UtcNow.AddDays(-1.0),
            //        ClassroomType = "MISTA",
            //        Lunch = "IOGURTE E BISCOITO",
            //        Educator = new Educator
            //        {
            //            FirstName = "JOSILENE"
            //        },
            //        Childs = new List<Child>
            //        {
            //            new Child
            //            {
            //                Id = 2, //Guid.NewGuid(),
            //                FullName = "GUILHERME GONÇALVE DIAS",
            //                BirthDate = DateTime.UtcNow.AddYears(-11),
            //                GenderType = "M",
            //                StartTimeMeeting = DateTime.UtcNow.AddDays(-1.0),
            //                EndTimeMeeting = DateTime.UtcNow.AddDays(-1.0).AddHours(2.0),
            //                Responsibles = new List<Responsible>
            //                {
            //                    new Responsible
            //                    {
            //                        Id = 2, //Guid.NewGuid(),
            //                        FullName = "RODRIGO GONÇALVES DIAS",
            //                        Cpf = new Cpf("439.599.527-67"),
            //                        KinshipType = "PAI",
            //                        Phones = new List<Phone>
            //                        {
            //                            new Phone
            //                            {
            //                                Number = "(21) 95598-0668"
            //                            }
            //                        }

            //                    }
            //                }

            //            }
            //        }
            //    }
            //};

            return new List<Classroom>();

        }
    }
}
