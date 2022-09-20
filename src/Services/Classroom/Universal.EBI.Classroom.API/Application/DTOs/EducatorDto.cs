using System;
using Universal.EBI.Classrooms.API.Application.Commands;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class EducatorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FunctionType { get; set; }
        public string PhotoUrl { get; set; }
        //public List<PhoneDto> Phones { get; set; }
        //public AddressDto Address { get; set; }
        //public Guid ClassroomId { get; set; }

        public Educator ToConvertEducator(EducatorDto educatorDto)
        {
            var educator = new Educator
            {                
                Id = educatorDto.Id,
                FirstName = educatorDto.FirstName,
                LastName = educatorDto.LastName,
                //FullName = educatorDto.FullName,
                //Email = educatorDto.Email == null ? null : new Email(educatorDto.Email.Address),
                //Cpf = educatorDto.Cpf == null ? null : new Cpf(educatorDto.Cpf.Number),
                //Phones = new List<Phone>(),
                //Address = new Address(),
                //BirthDate = string.IsNullOrWhiteSpace(educatorDto.BirthDate) ? DateTime.Now : DateTime.Parse(educatorDto.BirthDate).Date, 
                //GenderType = string.IsNullOrWhiteSpace(educatorDto.GenderType) ? 0 : (GenderType)Enum.Parse(typeof(GenderType), educatorDto.GenderType, true),
                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), educatorDto.FunctionType, true),                
                PhotoUrl = educatorDto.PhotoUrl,
                //Excluded = educatorDto.Excluded,
                //ClassroomId = educatorDto.ClassroomId
            };

            //if (educatorDto.Address != null)
            //{
            //    educator.Address = new Address
            //    {
            //        Id = educatorDto.Address.Id,
            //        PublicPlace = educatorDto.Address.PublicPlace,
            //        Number = educatorDto.Address.Number,
            //        Complement = educatorDto.Address.Complement,
            //        District = educatorDto.Address.District,
            //        City = educatorDto.Address.City,
            //        State = educatorDto.Address.State,
            //        ZipCode = educatorDto.Address.ZipCode,
            //        EducatorId = educatorDto.Address.ForeingKeyId
            //    };
            //}            

            //if (educatorDto.Phones != null)
            //{
            //    foreach (var phone in educatorDto.Phones)
            //    {
            //        educator.Phones.Add(new Phone
            //        {
            //            Id = phone.Id,
            //            Number = phone.Number,
            //            PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), phone.PhoneType, true),
            //            EducatorId = phone.ForeingKeyId
            //        });
            //    }
            //}            

            return educator;
        }        

    }   
    
}
