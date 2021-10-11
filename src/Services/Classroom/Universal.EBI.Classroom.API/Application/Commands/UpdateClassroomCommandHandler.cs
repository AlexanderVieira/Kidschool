using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Commands
{
    public class UpdateClassroomCommandHandler : CommandHandler, IRequestHandler<UpdateClassroomCommand, ValidationResult>
    {
        private readonly IClassroomRepository _ClassroomRepository;
        private readonly IClassroomQueries _ClassroomQueries;

        public UpdateClassroomCommandHandler(IClassroomRepository childRepository, IClassroomQueries childQueries)
        {
            _ClassroomRepository = childRepository;
            _ClassroomQueries = childQueries;
        }

        public async Task<ValidationResult> Handle(UpdateClassroomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            //var existingChild = await _ClassroomQueries.GetClassroomByCpf(message.Cpf);

            //existingChild.FirstName = message.FirstName;
            //existingChild.LastName = message.LastName;
            //existingChild.FullName = $"{message.FirstName} {message.LastName}";
            //existingChild.Email = ValidationUtils.ValidateRequestEmail(message.Email);
            //existingChild.Cpf = ValidationUtils.ValidateRequestCpf(message.Email);
            //existingChild.Phones = message.Phones;
            //existingChild.Address = message.Address;
            //existingChild.BirthDate = DateTime.Parse(message.BirthDate);
            //existingChild.GenderType = (GenderType)Enum.Parse(typeof(GenderType), message.Gender, true);
            //existingChild.KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), message.Kinship, true);
            //existingChild.PhotoUrl = message.PhotoUrl;
            //existingChild.Excluded = message.Excluded;
            //existingChild.Childs = message.Childs;

            //var success = await _ClassroomRepository.UpdateClassroom(existingChild);

            //existingChild.AddEvent(new UpdatedClassroomEvent
            //{
            //    AggregateId = message.Id,
            //    Id = message.Id,
            //    FirstName = message.FirstName,
            //    LastName = message.LastName,
            //    FullName = message.FullName,
            //    Email = message.Email,
            //    Cpf = message.Cpf,
            //    Phones = message.Phones,
            //    Address = message.Address,
            //    BirthDate = message.BirthDate,
            //    Gender = message.Gender,
            //    Kinship = message.Kinship,
            //    PhotoUrl = message.PhotoUrl,
            //    Excluded = message.Excluded,
            //    Childs = message.Childs
            //});

            return await PersistData(_ClassroomRepository.UnitOfWork, true);
        }
    }
}
