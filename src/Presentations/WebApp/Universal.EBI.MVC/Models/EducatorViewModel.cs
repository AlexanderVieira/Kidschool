using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.MVC.Models
{
    public class EducatorViewModel : PersonViewModel
    {   
        public string FunctionType { get; set; }        
        public List<PhoneViewModel> Phones { get; set; }
        public AddressViewModel Address { get; set; }
        public Guid ClassroomId { get; set; }

        public Educator ToConvertEducator(EducatorViewModel educatorDto)
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

    }   
    
}
