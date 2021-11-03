using System;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Educators.API.Application.Dtos
{
    public class EducatorClassroomDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }             
        public string Email { get; set; }
        public string Cpf { get; set; }        
        public string BirthDate { get; set; }
        public string GenderType { get; set; }
        public string FunctionType { get; set; }                    

        public EducatorClassroomDto ToConvertEducator(Educator educator)
        {
            var educatorDto = new EducatorClassroomDto
            {                
                Id = educator.Id,
                FirstName = educator.FirstName,
                LastName = educator.LastName,                
                Email = educator.Email.Address,
                Cpf = educator.Cpf.Number,                              
                BirthDate = educator.BirthDate.Date.ToShortDateString(), 
                GenderType = educator.GenderType.ToString(),
                FunctionType = educator.FunctionType.ToString()
                
            };

            return educatorDto;
        }

        public Educator ToConvertEducator(EducatorClassroomDto educatorDto)
        {
            var educator = new Educator
            {
                Id = educatorDto.Id,
                FirstName = educatorDto.FirstName,
                LastName = educatorDto.LastName,                
                Email = new Email(educatorDto.Email),
                Cpf = new Cpf(educatorDto.Cpf),                
                Address = new Address(),
                BirthDate = DateTime.Parse(educatorDto.BirthDate).Date,
                GenderType = (GenderType)Enum.Parse(typeof(GenderType), educatorDto.GenderType, true),
                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), educatorDto.FunctionType, true)
                
            };

            return educator;
        }

    }   
    
}
