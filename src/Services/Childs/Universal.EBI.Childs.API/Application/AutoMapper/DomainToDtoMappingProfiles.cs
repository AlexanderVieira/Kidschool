using AutoMapper;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Childs.API.Application.AutoMapper
{
    public class DomainToDtoMappingProfiles : Profile
    {
        public DomainToDtoMappingProfiles()
        {
            CreateMap<Child, ChildResponseDto>();
            //.ForPath(c => c.Cpf, opt => opt.MapFrom(s => s.Cpf))
            //.ForPath(c => c.Email, opt => opt.MapFrom(s => s.Email))

            CreateMap<Address, AddressDto>();

            CreateMap<Phone, PhoneResponseDto>();

            CreateMap<Responsible, ResponsibleDto>();
                //.ForPath(c => c.Cpf, opt => opt.MapFrom(s => s.Cpf))
                //.ForPath(c => c.Email, opt => opt.MapFrom(s => s.Email))
                
        }
    }
}
