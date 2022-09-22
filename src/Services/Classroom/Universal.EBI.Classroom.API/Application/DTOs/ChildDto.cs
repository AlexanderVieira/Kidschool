using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class ChildDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }        
        public string BirthDate { get; set; }
        public string AgeGroupType { get; set; }
        public string GenderType { get; set; }
        public List<ResponsibleDto> Responsibles { get; set; }

        public ChildDto ToConvertChildDto(Child child)
        {
            var childDto = new ChildDto
            {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                FullName = $"{child.FirstName} {child.LastName}",               
                BirthDate = child.BirthDate.ToShortDateString(), 
                GenderType = child.GenderType.ToString(), 
                AgeGroupType = child.AgeGroupType.ToString(),               
                Responsibles = new List<ResponsibleDto>()               
            };
                       
            int j = 0;
            foreach (var responsible in child.Responsibles)
            {
                childDto.Responsibles.Add(new ResponsibleDto
                {
                    Id = responsible.Id,
                    FirstName = responsible.FirstName,
                    LastName = responsible.LastName,
                    FullName = $"{responsible.FirstName} {responsible.LastName}",                    
                    Phones = new List<PhoneDto>(),                                    
                    BirthDate = responsible.BirthDate, //DateTime.Parse(responsible.BirthDate).Date,                    
                    KinshipType = responsible.KinshipType.ToString(), //(KinshipType)Enum.Parse(typeof(KinshipType), message.Kinship, true),
                    Cpf = responsible.Cpf.Number            
                    
                });                               

                foreach (var phone in childDto.Responsibles[j++].Phones)
                {
                    childDto.Responsibles[j++].Phones.Add(new PhoneDto
                    {
                        Id = phone.Id,
                        Number = phone.Number,
                        PhoneType = phone.PhoneType.ToString()                        
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
                BirthDate = DateTime.Parse(childDto.BirthDate).Date,
                GenderType = (GenderType)Enum.Parse(typeof(GenderType), childDto.GenderType, true),
                AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), childDto.AgeGroupType, true),                
                Responsibles = new List<Responsible>()                
            };
            
            int j = 0;            
            foreach (var responsibleDto in childDto.Responsibles)
            {
                child.Responsibles.Add(new Responsible
                {
                    Id = responsibleDto.Id,
                    FirstName = responsibleDto.FirstName,
                    LastName = responsibleDto.LastName,
                    FullName = $"{responsibleDto.FirstName} {responsibleDto.LastName}",                    
                    Cpf = ValidationUtils.ValidateRequestCpf(responsibleDto.Cpf),
                    Phones = new List<Phone>
                    {
                          new Phone
                          {
                                Id = responsibleDto.Phones[j].Id,
                                Number = responsibleDto.Phones[j].Number,
                                PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), responsibleDto.Phones[j].PhoneType, true)                                
                          }
                    },
                    
                    BirthDate = responsibleDto.BirthDate.Date, //DateTime.Parse(responsibleDto.BirthDate).Date,                     
                    KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), responsibleDto.KinshipType, true)                   

                });              

                j++;

            }

            return child;

        }

    }
}
