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
            .ForPath(c => c.Cpf, opt => opt.MapFrom(s => s.Cpf.Number))
            .ForPath(c => c.Email, opt => opt.MapFrom(s => s.Email.Address));

            CreateMap<Address, AddressResponseDto>();

            CreateMap<Phone, PhoneResponseDto>();

            CreateMap<Responsible, ResponsibleResponseDto>()
            .ForPath(c => c.Cpf, opt => opt.MapFrom(s => s.Cpf.Number))
            .ForPath(c => c.Email, opt => opt.MapFrom(s => s.Email.Address));

            CreateMap<ChildDesignedQuery, ChildDesignedQueryResponseDto>();

            CreateMap<Child, ChildRequestDto>()
            .ForPath(c => c.Cpf, opt => opt.MapFrom(s => s.Cpf.Number))
            .ForPath(c => c.Email, opt => opt.MapFrom(s => s.Email.Address));

            CreateMap<Address, AddressRequestDto>();

            CreateMap<Phone, PhoneRequestDto>();

            CreateMap<Responsible, ResponsibleRequestDto>()
            .ForPath(c => c.Cpf, opt => opt.MapFrom(s => s.Cpf.Number))
            .ForPath(c => c.Email, opt => opt.MapFrom(s => s.Email.Address));

        }
    }
}
