﻿using System;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.MVC.Models
{
    public class EducatorClassroomViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }             
        //public string Email { get; set; }
        //public string Cpf { get; set; }        
        public string BirthDate { get; set; }
        public string GenderType { get; set; }
        public string FunctionType { get; set; }     
                    

        public EducatorClassroomViewModel ToConvertEducator(Educator educator)
        {
            var educatorDto = new EducatorClassroomViewModel
            {                
                Id = educator.Id,
                FirstName = educator.FirstName,
                LastName = educator.LastName,                
                //Email = educator.Email.Address,
                //Cpf = educator.Cpf.Number,                              
                BirthDate = educator.BirthDate.Date.ToShortDateString(), 
                GenderType = educator.GenderType.ToString(),
                FunctionType = educator.FunctionType.ToString()
                
            };

            return educatorDto;
        }

        public Educator ToConvertEducator(EducatorClassroomViewModel educatorDto)
        {
            var educator = new Educator
            {
                Id = educatorDto.Id,
                FirstName = educatorDto.FirstName,
                LastName = educatorDto.LastName,                
                //Email = new Email(educatorDto.Email),
                //Cpf = new Cpf(educatorDto.Cpf),                
                //Address = new Address(),
                BirthDate = DateTime.Parse(educatorDto.BirthDate).Date,
                GenderType = (GenderType)Enum.Parse(typeof(GenderType), educatorDto.GenderType, true),
                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), educatorDto.FunctionType, true)
                
            };          

            return educator;
        }

    }   
    
}
