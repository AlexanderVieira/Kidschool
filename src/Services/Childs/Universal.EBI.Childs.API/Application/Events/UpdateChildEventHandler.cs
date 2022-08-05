using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Utils;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Application.Events
{
    public class UpdateChildEventHandler : INotificationHandler<UpdatedChildEvent>
    {
        private readonly IMessageBus _bus;
        private readonly IChildNoSqlRepository _childNoSqlRepository;

        public UpdateChildEventHandler(IMessageBus bus, IChildNoSqlRepository childNoSqlRepository)
        {
            _bus = bus;
            _childNoSqlRepository = childNoSqlRepository;
        }

        public Task Handle(UpdatedChildEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var child = new Child
                {
                    Id = notification.Id,
                    FirstName = notification.FirstName,
                    LastName = notification.LastName,
                    FullName = notification.FullName,
                    Email = ValidationUtils.ValidateRequestEmail(notification.Email),
                    Cpf = ValidationUtils.ValidateRequestCpf(notification.Cpf),
                    BirthDate = DateTime.Parse(notification.BirthDate),
                    GenderType = (GenderType)Enum.Parse(typeof(GenderType), notification.GenderType, true),
                    AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), notification.AgeGroupType, true),
                    PhotoUrl = notification.PhotoUrl,
                    Excluded = notification.Excluded,
                    CreatedBy = notification.CreatedBy,
                    CreatedDate = notification.CreatedDate,
                    LastModifiedBy = notification.LastModifiedBy,
                    LastModifiedDate = notification.LastModifiedDate,
                    Phones = notification.Phones,
                    Address = notification.Address,
                    Responsibles = notification.Responsibles

                };

                var result = _childNoSqlRepository.UpdateChild(child);

            }
            catch (Exception)
            {
                //Debug.WriteLine(ex.Message);                
                throw;
            }

            return Task.CompletedTask;
            //return _bus.PublishAsync(new UpdatedChildIntegrationEvent 
            //{ 
            //    Id = notification.Id 
            //});
        }
    }
}
