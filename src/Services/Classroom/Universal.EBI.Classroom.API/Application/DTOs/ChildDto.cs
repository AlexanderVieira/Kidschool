using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Classrooms.API.Models.Enums;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class ChildDto : PersonDto
    {
        public string StartTimeMeeting { get; set; }
        public string EndTimeMeeting { get; set; }        
        public int AgeGroupType { get; set; }
        public List<PhoneDto> Phones { get; set; }
        public List<ResponsibleDto> Responsibles { get; set; }

        public ChildDto ToConvertChildDto(Child child)
        {
            var childDto = new ChildDto
            {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                FullName = $"{child.FirstName} {child.LastName}",
                Email = ValidationUtils.ValidateRequestEmail(child.Email.Address),
                Cpf = ValidationUtils.ValidateRequestCpf(child.Cpf.Number),
                Phones = new List<PhoneDto>(),
                Address = new AddressDto(), //RegisterChildValidation.ValidateRequestAddress(message.Address),
                BirthDate = child.BirthDate.ToShortDateString(), //DateTime.Parse(child.BirthDate).Date,
                GenderType = (int)child.GenderType, //(GenderType)Enum.Parse(typeof(GenderType), message.Gender, true),
                AgeGroupType = (int)child.AgeGroupType, //(AgeGroupType)Enum.Parse(typeof(AgeGroupType), message.AgeGroup, true),
                PhotoUrl = child.PhotoUrl,
                Excluded = child.Excluded,
                Responsibles = new List<ResponsibleDto>(),
                StartTimeMeeting = child.StartTimeMeeting.ToString(),
                EndTimeMeeting = child.EndTimeMeeting.ToString()
            };

            childDto.Address = new AddressDto
            {
                Id = child.Address.Id,
                PublicPlace = child.Address.PublicPlace,
                Number = child.Address.Number,
                Complement = child.Address.Complement,
                District = child.Address.District,
                City = child.Address.City,
                State = child.Address.State,
                ZipCode = child.Address.ZipCode,
                ForeingKeyId = child.Address.ForeingKeyId
            };

            foreach (var phone in child.Phones)
            {
                childDto.Phones.Add(new PhoneDto 
                { 
                    Id = phone.Id,
                    Number = phone.Number, 
                    PhoneType = (int)phone.PhoneType,
                    ForeingKeyId = phone.ForeingKeyId
                });
            }

            int i = 0;
            int j = 0;
            foreach (var responsible in child.Responsibles)
            {
                childDto.Responsibles.Add(new ResponsibleDto
                {
                    Id = responsible.Id,
                    FirstName = responsible.FirstName,
                    LastName = responsible.LastName,
                    FullName = $"{responsible.FirstName} {responsible.LastName}",
                    Email = ValidationUtils.ValidateRequestEmail(responsible.Email.Address),
                    Cpf = ValidationUtils.ValidateRequestCpf(responsible.Cpf.Number),
                    Phones = new List<PhoneDto>(),
                    Address = new AddressDto(),                    
                    BirthDate = responsible.BirthDate.ToShortDateString(), //DateTime.Parse(responsible.BirthDate).Date,
                    GenderType = (int)responsible.GenderType, //(GenderType)Enum.Parse(typeof(GenderType), message.Gender, true),
                    KinshipType = (int)responsible.KinshipType, //(KinshipType)Enum.Parse(typeof(KinshipType), message.Kinship, true),
                    PhotoUrl = responsible.PhotoUrl,
                    Excluded = responsible.Excluded                    
                    
                });

                childDto.Responsibles[i++].Address = new AddressDto
                {
                    Id = responsible.Address.Id,
                    PublicPlace = responsible.Address.PublicPlace,
                    Number = responsible.Address.Number,
                    Complement = responsible.Address.Complement,
                    District = responsible.Address.District,
                    City = responsible.Address.City,
                    State = responsible.Address.State,
                    ZipCode = responsible.Address.ZipCode,
                    ForeingKeyId = responsible.Address.ForeingKeyId
                };

                foreach (var phone in childDto.Responsibles[j++].Phones)
                {
                    childDto.Responsibles[j++].Phones.Add(new PhoneDto
                    {
                        Id = phone.Id,
                        Number = phone.Number,
                        PhoneType = (int)phone.PhoneType,
                        ForeingKeyId = phone.ForeingKeyId
                    });
                }
                
            }

            return childDto;

        }

        public Child ToConvertChild(ChildDto childDto)
        {
            var child = new Child
            {
                Id = childDto.Id,
                FirstName = childDto.FirstName,
                LastName = childDto.LastName,
                FullName = $"{childDto.FirstName} {childDto.LastName}",
                Email = ValidationUtils.ValidateRequestEmail(childDto.Email.Address),
                Cpf = ValidationUtils.ValidateRequestCpf(childDto.Cpf.Number),
                Phones = new List<Phone>(),
                Address = new Address(), //RegisterChildValidation.ValidateRequestAddress(message.Address),
                BirthDate = DateTime.Parse(childDto.BirthDate).Date,
                GenderType = (GenderType)childDto.GenderType, //(GenderType)Enum.Parse(typeof(GenderType), message.Gender, true),
                AgeGroupType = (AgeGroupType)childDto.AgeGroupType, //(AgeGroupType)Enum.Parse(typeof(AgeGroupType), message.AgeGroup, true),
                PhotoUrl = childDto.PhotoUrl,
                Excluded = childDto.Excluded,
                Responsibles = new List<Responsible>(),
                StartTimeMeeting = DateTime.Parse(childDto.StartTimeMeeting).Date,
                EndTimeMeeting = childDto.EndTimeMeeting != null ? DateTime.Parse(childDto.EndTimeMeeting).Date : null
            };

            child.Address = new Address
            {
                Id = childDto.Address.Id,
                PublicPlace = childDto.Address.PublicPlace,
                Number = childDto.Address.Number,
                Complement = childDto.Address.Complement,
                District = childDto.Address.District,
                City = childDto.Address.City,
                State = childDto.Address.State,
                ZipCode = childDto.Address.ZipCode,
                ForeingKeyId = childDto.Address.ForeingKeyId
            };

            foreach (var phone in childDto.Phones)
            {
                child.Phones.Add(new Phone
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = (PhoneType)phone.PhoneType,
                    ForeingKeyId = phone.ForeingKeyId
                });
            }
                        
            int j = 0;            
            foreach (var responsibleDto in childDto.Responsibles)
            {
                child.Responsibles.Add(new Responsible
                {
                    Id = responsibleDto.Id,
                    FirstName = responsibleDto.FirstName,
                    LastName = responsibleDto.LastName,
                    FullName = $"{responsibleDto.FirstName} {responsibleDto.LastName}",
                    Email = ValidationUtils.ValidateRequestEmail(responsibleDto.Email.Address),
                    Cpf = ValidationUtils.ValidateRequestCpf(responsibleDto.Cpf.Number),
                    Phones = new List<Phone> 
                    {
                          new Phone
                          {
                                Id = responsibleDto.Phones[j].Id, 
                                Number = responsibleDto.Phones[j].Number, 
                                PhoneType = (PhoneType)responsibleDto.Phones[j].PhoneType,
                                ForeingKeyId = responsibleDto.Phones[j].ForeingKeyId 
                          }                  
                    },
                    Address = new Address(),
                    BirthDate = DateTime.Parse(responsibleDto.BirthDate).Date, 
                    GenderType = (GenderType)responsibleDto.GenderType, 
                    KinshipType = (KinshipType)responsibleDto.KinshipType,
                    PhotoUrl = responsibleDto.PhotoUrl,
                    Excluded = responsibleDto.Excluded

                });

                child.Responsibles.ToList()[j].Address = new Address
                {
                    Id = responsibleDto.Address.Id,
                    PublicPlace = responsibleDto.Address.PublicPlace,
                    Number = responsibleDto.Address.Number,
                    Complement = responsibleDto.Address.Complement,
                    District = responsibleDto.Address.District,
                    City = responsibleDto.Address.City,
                    State = responsibleDto.Address.State,
                    ZipCode = responsibleDto.Address.ZipCode,
                    ForeingKeyId = responsibleDto.Address.ForeingKeyId
                };

                j++;

            }

            return child;

        }

    }
}
