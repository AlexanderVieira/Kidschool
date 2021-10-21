using System;
using System.Collections.Generic;
using Universal.EBI.Classrooms.API.Application.Commands;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class EducatorDto : PersonDto
    {   
        public string FunctionType { get; set; }        
        public List<PhoneDto> Phones { get; set; }
        public AddressDto Address { get; set; }
        public Guid ClassroomId { get; set; }

        public Educator ToConvertEducator(EducatorDto educatorDto)
        {
            var educator = new Educator
            {                
                Id = educatorDto.Id,
                FirstName = educatorDto.FirstName,
                LastName = educatorDto.LastName,
                FullName = educatorDto.FullName,
                Email = new Email(educatorDto.Email.Address),
                Cpf = new Cpf(educatorDto.Cpf.Number),
                Phones = new List<Phone>(),
                Address = new Address(),
                BirthDate = DateTime.Parse(educatorDto.BirthDate).Date, 
                GenderType = (GenderType)Enum.Parse(typeof(GenderType), educatorDto.GenderType, true),
                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), educatorDto.FunctionType, true),                
                PhotoUrl = educatorDto.PhotoUrl,
                Excluded = educatorDto.Excluded,
                ClassroomId = educatorDto.ClassroomId
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
                EducatorId = educatorDto.Address.ForeingKeyId
            };

            foreach (var phone in educatorDto.Phones)
            {
                educator.Phones.Add(new Phone
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), phone.PhoneType, true),                    
                    EducatorId = phone.ForeingKeyId
                });
            }

            return educator;
        }

        public EducatorDto ConvertRegisterCommandToEducatorDto(RegisterClassroomCommand command)
        {            
            var EducatorDto = new EducatorDto
            {
                Id = command.Educator.Id,
                FirstName = command.Educator.FirstName,
                LastName = command.Educator.LastName,
                FullName = command.Educator.FullName,
                Email = new Email(command.Educator.Email.Address),
                Cpf = new Cpf(command.Educator.Cpf.Number),
                Phones = new List<PhoneDto>(),
                Address = new AddressDto(),
                BirthDate = command.Educator.BirthDate, 
                GenderType = command.Educator.GenderType.ToString(), 
                FunctionType = command.Educator.FunctionType.ToString(),
                PhotoUrl = command.Educator.PhotoUrl,
                Excluded = command.Educator.Excluded,
                CreatedDate = command.CreatedDate,
                CreatedBy = command.CreatedBy,
                ClassroomId = command.Educator.ClassroomId
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
                    PhoneType = phone.PhoneType.ToString(),
                    ForeingKeyId = phone.ForeingKeyId
                });
            }

            return EducatorDto;
        }

        public EducatorDto ConvertUpdateCommandToEducatorDto(UpdateClassroomCommand command)
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
                BirthDate = command.Educator.BirthDate,
                GenderType = command.Educator.GenderType.ToString(),
                FunctionType = command.Educator.FunctionType.ToString(),
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
                    PhoneType = phone.PhoneType.ToString(),
                    ForeingKeyId = phone.ForeingKeyId
                });
            }

            return EducatorDto;
        }        

    }   
    
}
