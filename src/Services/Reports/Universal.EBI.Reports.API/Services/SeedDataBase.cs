using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Models.Enums;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Reports.API.Data;
using Universal.EBI.Reports.API.Models;
using Universal.EBI.Reports.API.Models.Enums;

namespace Universal.EBI.Reports.API.Services
{
    public class SeedDataBase : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedDataBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InsertDataBase();
            return Task.CompletedTask;
        }

        public void InsertDataBase()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ReportContext>();
                InsertData(context);
            }
        }
        public void InsertData(IApplicationBuilder app)
        {
            InsertData(app.ApplicationServices.GetRequiredService<ReportContext>());
        }

        public void InsertData(ReportContext ctx)
        {
            //_logger.LogDebug("Infra.Data.Sql.MySql :: InsertData() :: Aplicando Migrations... " + DateTime.Now.ToLongTimeString());
            Debug.WriteLine("InsertData() :: Aplicando Migrations... " + DateTime.Now.ToLongTimeString());
            ctx.Database.Migrate();
            if (!ctx.Classrooms.Any())
            {
                //_logger.LogDebug("Infra.Data.Sql.MySql :: InsertData() :: Criando Dados... " + DateTime.Now.ToLongTimeString());
                Debug.WriteLine("InsertData() :: Criando Dados... " + DateTime.Now.ToLongTimeString());
                //ctx.Educators.AddRange
                //    (
                //        new Educator
                //        {
                //            Id = Guid.Parse("9b9e4944-b91f-41e9-a8d8-252c9ec7f815"),
                //            FirstName = "Josilene",
                //            LastName = "Silva de Sales",
                //            FullName = "Josilene Silva de Sales",
                //            FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), "Auxiliary", true),
                //            GenderType = (GenderType)Enum.Parse(typeof(GenderType), "F", true),
                //            Excluded = false,
                //            ClassroomId = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a")
                //        },
                //         new Educator
                //         {
                //             Id = Guid.Parse("1a933de8-c3fd-4898-900e-b0328bed569d"),
                //             FirstName = "Sebastiana",
                //             LastName = "Ferreira da Silva",
                //             FullName = "Sebastiana Ferreira da Silva",
                //             FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), "Responsible", true),
                //             GenderType = (GenderType)Enum.Parse(typeof(GenderType), "F", true),
                //             Excluded = false,
                //             ClassroomId = Guid.Parse("83a18529-b917-400d-a614-ebca205659aa")
                //         }
                //    );

                //ctx.Responsibles.AddRange
                //    (
                //        new Responsible
                //        {
                //            Id = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f"),
                //            Cpf = new Cpf("04138000755"),
                //            FirstName = "Alexander",
                //            LastName = "Vieira da Silva",
                //            FullName = "Alexander Vieira da Silva",
                //            KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
                //            GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                //            Excluded = false,
                //            Phones = new List<Phone>
                //            {
                //                new Phone { Id = Guid.Parse("036fd4ec-715f-48bd-be9c-05b4e1d08807"), Number = "21965200293", ResponsibleId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f") }
                //            }
                //        },
                //         new Responsible
                //         {
                //             Id = Guid.Parse("ab4dffb9-6037-4c57-9bf9-20e02abf04d9"),
                //             Cpf = new Cpf("19100000000"),
                //             FirstName = "Rodrigo",
                //             LastName = "Gonçalves Dias",
                //             FullName = "Rodrigo Gonçalves Dias",
                //             KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
                //             GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                //             Excluded = false,
                //             Phones = new List<Phone>
                //             {
                //                 new Phone { Id = Guid.Parse("afd70922-a665-475f-893f-9284a56eff10"), Number = "21985200222", ResponsibleId = Guid.Parse("ab4dffb9-6037-4c57-9bf9-20e02abf04d9") }
                //             }
                //         }
                //    );

                //ctx.Children.AddRange
                //    (
                //        new Child
                //        {
                //            Id = Guid.Parse("e0c7a17a-d202-4d34-a31b-1b0f348a3e1f"),
                //            FirstName = "Jonathan",
                //            LastName = "de Sales da Silva",
                //            FullName = "Jonathan de Sales da Silva",
                //            BirthDate = new DateTime(2012, 8, 29).Date,
                //            GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                //            AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), "Juniors", true),
                //            HoraryOfEntry = DateTime.UtcNow.ToString("HH:mm"), //DateTime.Parse(DateTime.UtcNow.ToString("HH:mm:ss")),
                //            HoraryOfExit = DateTime.UtcNow.AddHours(2.0).ToString("HH:mm"), //DateTime.Parse(DateTime.UtcNow.AddHours(1.5).ToString("HH:mm:ss")),
                //            ClassroomId = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a"),
                //            Excluded = false,
                //            Responsibles = new List<Responsible>
                //            {
                //                new Responsible
                //                {
                //                    Id = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f"),
                //                    Cpf = new Cpf("04138000755"),
                //                    FirstName = "Alexander",
                //                    LastName = "Vieira da Silva",
                //                    FullName = "Alexander Vieira da Silva",
                //                    KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
                //                    GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                //                    Excluded = false,
                //                    Phones = new List<Phone>
                //                    {
                //                        new Phone 
                //                        { 
                //                            Id = Guid.Parse("036fd4ec-715f-48bd-be9c-05b4e1d08807"), 
                //                            Number = "21965200293",
                //                            PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true),
                //                            ResponsibleId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f") 
                //                        }
                //                    }
                //                }
                //            }
                //        },
                //        new Child
                //        {
                //            Id = Guid.Parse("7d6dff47-6013-4044-8570-a3e0ded6a32d"),
                //            FirstName = "Guilherme",
                //            LastName = "Gonçalves Dias",
                //            FullName = "Guilherme Gonçalves Dias",
                //            BirthDate = new DateTime(2014, 2, 9).Date,
                //            GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                //            AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), "Juniors", true),
                //            HoraryOfEntry = DateTime.UtcNow.ToString("HH:mm"), //DateTime.Parse(DateTime.UtcNow.ToString("HH:mm:ss")),
                //            HoraryOfExit = DateTime.UtcNow.AddHours(2.0).ToString("HH:mm"), //DateTime.Parse(DateTime.UtcNow.AddHours(1.5).ToString("HH:mm:ss")),
                //            ClassroomId = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a"),
                //            Excluded = false,
                //            Responsibles = new List<Responsible>
                //            {
                //                new Responsible
                //                {
                //                    Id = Guid.Parse("ab4dffb9-6037-4c57-9bf9-20e02abf04d9"),
                //                    Cpf = new Cpf("19100000000"),
                //                    FirstName = "Rodrigo",
                //                    LastName = "Gonçalves Dias",
                //                    FullName = "Rodrigo Gonçalves Dias",
                //                    KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
                //                    GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                //                    Excluded = false,
                //                    Phones = new List<Phone>
                //                    {
                //                        new Phone 
                //                        { 
                //                            Id = Guid.Parse("afd70922-a665-475f-893f-9284a56eff10"), 
                //                            Number = "21985200222",
                //                            PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true),
                //                            ResponsibleId = Guid.Parse("ab4dffb9-6037-4c57-9bf9-20e02abf04d9") 
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    );

                //ctx.ChildResponsible.AddRange
                //    (
                //        new ChildResponsible
                //        {
                //            ResponsiblesId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f"),
                //            ChildrenId = Guid.Parse("e0c7a17a-d202-4d34-a31b-1b0f348a3e1f")
                //        },
                //        new ChildResponsible
                //        {
                //            ResponsiblesId = Guid.Parse("ab4dffb9-6037-4c57-9bf9-20e02abf04d9"),
                //            ChildrenId = Guid.Parse("7d6dff47-6013-4044-8570-a3e0ded6a32d")
                //        }
                //    );


                //ctx.Phones.AddRange
                //    (
                //         new Phone
                //         {
                //             Id = Guid.Parse("5b03891a-dbe2-45ac-81b9-381b381ca01d"),
                //             Number = "21965200293",
                //             PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true),
                //             ResponsibleId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f")
                //         },
                //         new Phone
                //         {
                //             Id = Guid.Parse("f643b38c-f2c2-4065-a124-602d994e0de5"),
                //             Number = "21998500222",
                //             PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true),
                //             ResponsibleId = Guid.Parse("ab4dffb9-6037-4c57-9bf9-20e02abf04d9")
                //         }
                //    );

                ctx.Classrooms.AddRange
                    (
                         new Classroom
                         {
                             Id = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a"),
                             Region = "São João I",
                             Church = "São Mateus II",
                             CreatedAt = DateTime.UtcNow.Date.AddDays(-1.0),
                             MeetingTime = DateTime.UtcNow.ToString("HH:mm"),
                             ClassroomType = (ClassroomType)Enum.Parse(typeof(ClassroomType), "Mixed", true),
                             Lunch = "Iogurt e Biscoito",
                             Educator = new Educator
                             {
                                 Id = Guid.Parse("9b9e4944-b91f-41e9-a8d8-252c9ec7f815"),
                                 FirstName = "Josilene",
                                 LastName = "Silva de Sales",
                                 FullName = "Josilene Silva de Sales",
                                 Cpf = "83882386711",
                                 FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), "Auxiliary", true),
                                 GenderType = (GenderType)Enum.Parse(typeof(GenderType), "F", true),
                                 Excluded = false,
                                 BirthDate = new DateTime(1980, 9, 10).Date,
                                 CreatedAt = DateTime.UtcNow.Date.AddDays(-10.0),
                                 ClassroomId = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a")
                             },
                             Children = new List<Child>
                             {
                                 new Child
                                 {
                                     Id = Guid.Parse("e0c7a17a-d202-4d34-a31b-1b0f348a3e1f"),
                                     FirstName = "Jonathan",
                                     LastName = "de Sales da Silva",
                                     FullName = "Jonathan de Sales da Silva",
                                     BirthDate = new DateTime(2012, 8, 29).Date,
                                     GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                                     AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), "Juniors", true),
                                     HoraryOfEntry = DateTime.UtcNow.ToString("HH:mm"), //DateTime.Parse(DateTime.UtcNow.ToString("HH:mm:ss")),
                                     HoraryOfExit = DateTime.UtcNow.AddHours(2.0).ToString("HH:mm"), //DateTime.Parse(DateTime.UtcNow.AddHours(1.5).ToString("HH:mm:ss")),
                                     ClassroomId = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a"),
                                     Excluded = false,
                                     Responsibles = new List<Responsible>
                                     {
                                         new Responsible
                                         {
                                             Id = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f"),
                                             Cpf = "04138000755",
                                             FirstName = "Alexander",
                                             LastName = "Vieira da Silva",
                                             FullName = "Alexander Vieira da Silva",
                                             KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
                                             GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                                             Excluded = false,
                                             Phones = new List<Phone>
                                             {
                                                 new Phone
                                                 {
                                                     Id = Guid.Parse("036fd4ec-715f-48bd-be9c-05b4e1d08807"),
                                                     Number = "21965200293",
                                                     PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true),
                                                     ResponsibleId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f")
                                                 }
                                             }
                                         }
                                     }
                                 }
                             }
                         },
                         new Classroom
                         {
                             Id = Guid.Parse("83a18529-b917-400d-a614-ebca205659aa"),
                             Region = "São João I",
                             Church = "São Mateus II",
                             CreatedAt = DateTime.UtcNow.Date.AddDays(-4.0),
                             MeetingTime = DateTime.UtcNow.ToString("HH:mm"),
                             ClassroomType = (ClassroomType)Enum.Parse(typeof(ClassroomType), "Mixed", true),
                             Lunch = "Iogurt e Biscoito",
                             Educator = new Educator
                             {
                                 Id = Guid.Parse("1a933de8-c3fd-4898-900e-b0328bed569d"),
                                 FirstName = "Sebastiana",
                                 LastName = "Ferreira da Silva",
                                 FullName = "Sebastiana Ferreira da Silva",
                                 Cpf = "97299625709",
                                 FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), "Responsible", true),
                                 GenderType = (GenderType)Enum.Parse(typeof(GenderType), "F", true),
                                 Excluded = false,
                                 BirthDate = new DateTime(1959, 4, 19).Date,
                                 CreatedAt = DateTime.UtcNow.Date.AddDays(-10.0),
                                 ClassroomId = Guid.Parse("83a18529-b917-400d-a614-ebca205659aa")
                             },
                             Children = new List<Child>
                             {
                                 new Child
                                 {
                                     Id = Guid.Parse("7d6dff47-6013-4044-8570-a3e0ded6a32d"),
                                     FirstName = "Guilherme",
                                     LastName = "Gonçalves Dias",
                                     FullName = "Guilherme Gonçalves Dias",
                                     BirthDate = new DateTime(2014, 2, 9).Date,
                                     GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                                     AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), "Juniors", true),
                                     HoraryOfEntry = DateTime.UtcNow.ToString("HH:mm"), //DateTime.Parse(DateTime.UtcNow.ToString("HH:mm:ss")),
                                     HoraryOfExit = DateTime.UtcNow.AddHours(2.0).ToString("HH:mm"), //DateTime.Parse(DateTime.UtcNow.AddHours(1.5).ToString("HH:mm:ss")),
                                     ClassroomId = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a"),
                                     Excluded = false,
                                     Responsibles = new List<Responsible>
                                     {
                                         new Responsible
                                         {
                                             Id = Guid.Parse("ab4dffb9-6037-4c57-9bf9-20e02abf04d9"),
                                             Cpf = "19100000000",
                                             FirstName = "Rodrigo",
                                             LastName = "Gonçalves Dias",
                                             FullName = "Rodrigo Gonçalves Dias",
                                             KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
                                             GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                                             Excluded = false,
                                             Phones = new List<Phone>
                                             {
                                                 new Phone
                                                 {
                                                     Id = Guid.Parse("afd70922-a665-475f-893f-9284a56eff10"),
                                                     Number = "21985200222",
                                                     PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true),
                                                     ResponsibleId = Guid.Parse("ab4dffb9-6037-4c57-9bf9-20e02abf04d9")
                                                 }
                                             }
                                         }
                                     }
                                 }
                             }
                         }
                    );

                ctx.SaveChanges();
            }
            else
            {
                Debug.WriteLine("InsertData() :: Dados já existem... " + DateTime.Now.ToLongTimeString());
                //_logger.LogDebug("Infra.Data.Sql.MySql :: InsertData() :: Dados já existem... " + DateTime.Now.ToLongTimeString());
            }
        }

    }
}
