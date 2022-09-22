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

        public Educator ToConvertEducator(EducatorDto educatorDto)
        {
            var educator = new Educator
            {                
                Id = educatorDto.Id,
                FirstName = educatorDto.FirstName,
                LastName = educatorDto.LastName,                
                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), educatorDto.FunctionType, true),                
                PhotoUrl = educatorDto.PhotoUrl,                
            };
            
            return educator;
        }

        public EducatorDto ToConvertEducatorDto(Educator educator)
        {
            var educatorDto = new EducatorDto
            {
                Id = educator.Id,
                FirstName = educator.FirstName,
                LastName = educator.LastName,
                FunctionType = educator.FunctionType.ToString(),
                PhotoUrl = educator.PhotoUrl,
            };

            return educatorDto;
        }

    }   
    
}
