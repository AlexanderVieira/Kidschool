using AutoMapper;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Models;

namespace Universal.EBI.Childs.API.Application.AutoMapper
{
    public class DomainToDtoMappingProfiles : Profile
    {
        public DomainToDtoMappingProfiles()
        {
            CreateMap<Child, ChildResponseDto>()
            .ForPath(c => c.NumberCpf, opt => opt.MapFrom(s => s.Cpf.Number))
            .ForPath(c => c.AddressEmail, opt => opt.MapFrom(s => s.Email));

            CreateMap<Address, AddressResponseDto>();

            CreateMap<Phone, PhoneResponseDto>();

            CreateMap<Responsible, ResponsibleResponseDto>()
            .ForPath(c => c.NumberCpf, opt => opt.MapFrom(s => s.Cpf.Number))
            .ForPath(c => c.AddressEmail, opt => opt.MapFrom(s => s.Email.Address));          
            
        }
    }
}
