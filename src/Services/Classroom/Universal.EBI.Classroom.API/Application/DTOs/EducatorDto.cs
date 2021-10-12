using System;
using System.Collections.Generic;
using Universal.EBI.Classrooms.API.Application.Commands;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Classrooms.API.Models.Enums;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class EducatorDto : PersonDto
    {   
        public int FunctionType { get; set; }        
        public List<PhoneDto> Phones { get; set; }        

        public Educator ToConvertEducator(EducatorDto educatorDto)
        {
            var educator = new Educator
            {                
                Id = educatorDto.Id,
                FirstName = educatorDto.FirstName,
                LastName = educatorDto.LastName,
                Email = new Email(educatorDto.Email.Address),
                Cpf = new Cpf(educatorDto.Cpf.Number),
                Phones = new List<Phone>(),
                Address = new Address(),
                BirthDate = DateTime.Parse(educatorDto.BirthDate).Date, //DateTime.Parse(educatorDto.BirthDate),
                GenderType = (GenderType)educatorDto.GenderType, //(Gender)Enum.Parse(typeof(Gender), message.Gender, true),
                FunctionType = (FunctionType)educatorDto.FunctionType, //(FunctionType)Enum.Parse(typeof(FunctionType), message.Function, true),
                PhotoUrl = educatorDto.PhotoUrl,
                Excluded = educatorDto.Excluded
            };

            educator.Address = new Address
            {
                Id = educatorDto.Address.Id,
                PublicPlace = educatorDto.Address.PublicPlace,
                Number = educatorDto.Address.Number,
                Complement = educatorDto.Address.Complement,
                District = educatorDto.Address.District,
                City = educatorDto.Address.City,
                State = educatorDto.Address.State,
                ZipCode = educatorDto.Address.ZipCode,
                ForeingKeyId = educatorDto.Address.ForeingKeyId
            };

            foreach (var phone in educatorDto.Phones)
            {
                educator.Phones.Add(new Phone
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = (PhoneType)phone.PhoneType,
                    ForeingKeyId = phone.ForeingKeyId
                });
            }

            return educator;
        }

        public EducatorDto ToConvertEducatorDto(RegisterClassroomCommand command)
        {
            var EducatorDto = new EducatorDto
            {
                Id = command.Educator.Id,
                FirstName = command.Educator.FirstName,
                LastName = command.Educator.LastName,
                Email = new Email(command.Educator.Email.Address),
                Cpf = new Cpf(command.Educator.Cpf.Number),
                Phones = new List<PhoneDto>(),
                Address = new AddressDto(),
                BirthDate = command.Educator.BirthDate, //DateTime.Parse(educatorDto.BirthDate),
                GenderType = (int)command.Educator.GenderType, //(Gender)Enum.Parse(typeof(Gender), message.Gender, true),
                FunctionType = (int)command.Educator.FunctionType, //(FunctionType)Enum.Parse(typeof(FunctionType), message.Function, true),
                PhotoUrl = command.Educator.PhotoUrl,
                Excluded = command.Educator.Excluded
            };

            EducatorDto.Address = new AddressDto
            {
                Id = command.Educator.Address.Id,
                PublicPlace = command.Educator.Address.PublicPlace,
                Number = command.Educator.Address.Number,
                Complement = command.Educator.Address.Complement,
                District = command.Educator.Address.District,
                City = command.Educator.Address.City,
                State = command.Educator.Address.State,
                ZipCode = command.Educator.Address.ZipCode,
                ForeingKeyId = command.Educator.Address.ForeingKeyId
            };

            foreach (var phone in command.Educator.Phones)
            {
                EducatorDto.Phones.Add(new PhoneDto
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = (int)phone.PhoneType,
                    ForeingKeyId = phone.ForeingKeyId
                });
            }

            return EducatorDto;
        }

    }   
    
}
