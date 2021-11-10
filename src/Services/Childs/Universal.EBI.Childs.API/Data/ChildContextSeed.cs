using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Childs.API.Data
{
    public class ChildContextSeed
    {
        public static void SeedData(IMongoCollection<Child> ChildCollection)
        {
            bool existChild = ChildCollection.Find(p => true).Any();
            if (!existChild)
            {
                ChildCollection.InsertMany(GetPreconfiguredChilds());
            }
        }

        private static IEnumerable<Child> GetPreconfiguredChilds()
        {
            return new List<Child>()
            {
                new Child()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jonathan",
                    LastName = "de Sales da Silva",
                    FullName = "Jonathan de Sales da Silva",
                    Address = new Address(),
                    AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), "Juniors", true),
                    BirthDate = new DateTime(2012, 8, 29).Date,
                    Cpf = null,
                    Email = null,
                    GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    HoraryOfEntry = null,
                    HoraryOfExit = null,
                    LastModifiedDate = null,
                    LastModifiedBy = null,
                    Excluded = false,
                    Responsibles = new List<Responsible>
                    { 
                        new Responsible 
                        { 
                            Id = Guid.NewGuid(), 
                            FirstName = "Alexander", 
                            LastName = "Vieira da Silva", 
                            FullName = "Alexander Vieira da Silva",
                            Address= new Address(),
                            BirthDate = new DateTime(1976,10,18).Date,
                            Cpf = new Cpf("30641168780"),
                            Email = new Email("alexander@teste.com"),
                            CreatedDate = DateTime.Now,
                            CreatedBy = "Admin",
                            Excluded = false,
                            GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                            KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
                            Phones = new List<Phone>
                            { 
                                new Phone 
                                { 
                                    Id = Guid.NewGuid(), 
                                    Number = "21966296441", 
                                    PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true) 
                                } 
                            },
                            LastModifiedDate = null,
                            LastModifiedBy = null,
                            PhotoUrl = null,
                            Children = new List<Child>()
                        }
                    },
                    Phones = new List<Phone>(),
                    PhotoUrl = null,
                    ClassroomId = null
                    
                },
                new Child()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Miguel",
                    LastName = "Gonçalves",
                    FullName = "Miguel Gonçalves",
                    Address = new Address(),
                    AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), "Juniors", true),
                    BirthDate = new DateTime(2015, 1, 01).Date,
                    Cpf = null,
                    Email = null,
                    GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    HoraryOfEntry = null,
                    HoraryOfExit = null,
                    LastModifiedDate = null,
                    LastModifiedBy = null,
                    Excluded = false,
                    Responsibles = new List<Responsible>
                    {
                        new Responsible
                        {
                            Id = Guid.NewGuid(),
                            FirstName = "Diego",
                            LastName = "Campanha",
                            FullName = "Diego Campanha",
                            Address= new Address(),
                            BirthDate = new DateTime(1984,1,01).Date,
                            Cpf = new Cpf("46526258719"),
                            Email = new Email("diego@teste.com"),
                            CreatedDate = DateTime.Now,
                            CreatedBy = "Admin",
                            Excluded = false,
                            GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                            KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
                            Phones = new List<Phone>
                            {
                                new Phone
                                {
                                    Id = Guid.NewGuid(),
                                    Number = "21966306442",
                                    PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true)
                                }
                            },
                            LastModifiedDate = null,
                            LastModifiedBy = null,
                            PhotoUrl = null,
                            Children = new List<Child>()
                        }
                    },
                    Phones = new List<Phone>(),
                    PhotoUrl = null,
                    ClassroomId = null

                },
                new Child()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Guilherme",
                    LastName = "Gonçalves Dias",
                    FullName = "Guilherme Gonçalves Dias",
                    Address = new Address(),
                    AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), "Juniors", true),
                    BirthDate = new DateTime(2014, 2, 01).Date,
                    Cpf = null,
                    Email = null,
                    GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    HoraryOfEntry = null,
                    HoraryOfExit = null,
                    LastModifiedDate = null,
                    LastModifiedBy = null,
                    Excluded = false,
                    Responsibles = new List<Responsible>
                    {
                        new Responsible
                        {
                            Id = Guid.NewGuid(),
                            FirstName = "Rodrigo",
                            LastName = "Gonçalves Dias",
                            FullName = "Rodrigo Gonçalves Dias",
                            Address= new Address(),
                            BirthDate = new DateTime(1976,10,18).Date,
                            Cpf = new Cpf("66736913799"),
                            Email = new Email("rodrigo@teste.com"),
                            CreatedDate = DateTime.Now,
                            CreatedBy = "Admin",
                            Excluded = false,
                            GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
                            KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
                            Phones = new List<Phone>
                            {
                                new Phone
                                {
                                    Id = Guid.NewGuid(),
                                    Number = "21966316446",
                                    PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true)
                                }
                            },
                            LastModifiedDate = null,
                            LastModifiedBy = null,
                            PhotoUrl = null,
                            Children = new List<Child>()
                        }
                    },
                    Phones = new List<Phone>(),
                    PhotoUrl = null,
                    ClassroomId = null

                }
            };
        }
    }
}
