using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.Events;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Classrooms.API.Models.Enums;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class RegisterClassroomCommandHandler : CommandHandler, IRequestHandler<RegisterClassroomCommand, ValidationResult>
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IClassroomQueries _ClassroomQueries;

        public RegisterClassroomCommandHandler(IClassroomRepository classroomRepository, IClassroomQueries classroomQueries)
        {
            _classroomRepository = classroomRepository;
            _ClassroomQueries = classroomQueries;
        }

        public async Task<ValidationResult> Handle(RegisterClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var classroom = new Classroom
            {
                Id = Guid.NewGuid(),
                Educator = message.Educator,
                Church = "SÃO MATHEUS II",
                Region = "SÃO JOÃO I",
                ClassroomType = (ClassroomType)Enum.Parse(typeof(ClassroomType), message.ClassroomType, true),
                Actived = message.Actived,
                MeetingTime = message.MeetingTime,
                Childs = message.Childs.ToDictionary(c => c.Id.ToString())                
            };

            //var existingClassroom = await _ClassroomQueries.GetClassroomById(classroom.Id);

            //if (existingClassroom != null)
            //{
            //    AddError("Esta sala já está em uso.");
            //    return ValidationResult;
            //}

            await _classroomRepository.CreateClassroom(classroom);
            var createdClassroom = await _ClassroomQueries.GetClassroomById(classroom.Id);
            var success = createdClassroom != null;            

            classroom.AddEvent(new RegisteredClassroomEvent 
            { 
                AggregateId = classroom.Id,
                Educator = classroom.Educator,
                Church = "SÃO MATHEUS II",
                Region = "SÃO JOÃO I",
                ClassroomType = classroom.ClassroomType.ToString(),
                Actived = classroom.Actived,
                MeetingTime = classroom.MeetingTime,
                Childs = classroom.Childs.Values.ToArray()

            });
            
            return await PersistData(_classroomRepository.UnitOfWork, success);
        }

        //private Classroom SeedClasseroom()
        //{
        //    if (classroomQueue.Count == 1)
        //    {
        //        return CustomResponse();
        //    }
        //    var classroom = new Classroom
        //    {
        //        Id = Guid.Empty,
        //        Educator = new Educator { FullName = "Josilene Silva de Sales", FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), "Classroom", true) },
        //        Church = "SÃO MATHEUS II",
        //        Region = "SÃO JOÃO I",
        //        ClassroomType = (ClassroomType)Enum.Parse(typeof(ClassroomType), "MISTA ", true),
        //        Actived = true
        //    };

        //    classroom.Childs.Enqueue(new Child
        //    {
        //        FullName = "Jonathan de Sales da Silva",
        //        GenderType = (GenderType)Enum.Parse(typeof(GenderType), "Male", true),
        //        BirthDate = new DateTime(2012, 08, 29).Date,
        //        StartTimeMeeting = DateTime.Now.Date,
        //        Classrooms = new List<Classroom>()
        //        {
        //            new Classroom
        //            {
        //                FullName = "Alexander Vieira da Silva",
        //                Cpf = new Cpf("041.380.007-55"),
        //                KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
        //                Phones = new List<Phone>() {
        //                    new Phone
        //                    {
        //                        Number = "(21) 96629-6441",
        //                        PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true)
        //                    }
        //                }
        //            }
        //        }
        //    });

        //    classroom.Childs.Enqueue(new Child
        //    {
        //        FullName = "Guilherme Gonçalves",
        //        GenderType = (GenderType)Enum.Parse(typeof(GenderType), "Male", true),
        //        BirthDate = new DateTime(2014, 05, 10).Date,
        //        StartTimeMeeting = DateTime.Now.Date,
        //        Classrooms = new List<Classroom>()
        //        {
        //            new Classroom
        //            {
        //                FullName = "Rodrigo Gonçalves",
        //                Cpf = new Cpf("857.818.577-37"),
        //                KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
        //                Phones = new List<Phone>() {
        //                    new Phone
        //                    {
        //                        Number = "(21) 96549-6430",
        //                        PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true)
        //                    }
        //                }
        //            }
        //        }
        //    });

        //    classroomQueue.Enqueue(classroom);

        //    return new Classroom();
        //}
    }
}
