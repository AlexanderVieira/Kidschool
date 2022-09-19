using AutoMapper;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Models;

namespace Universal.EBI.Childs.API.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<ChildRequestDto, Child>()
                .ForPath(c => c.Cpf, opt => opt.MapFrom(s => s.Cpf))
                .ForPath(c => c.Email, opt => opt.MapFrom(s => s.Email));
                //.ForPath(c => c.AgeGroupType, opt => opt.MapFrom(s => s.AgeGroupType))
                //.ForPath(c => c.GenderType, opt => opt.MapFrom(s => s.GenderType));

            CreateMap<AddressRequestDto, Address>();

            CreateMap<PhoneRequestDto, Phone>();
            //.ForPath(c => c.PhoneType, opt => opt.MapFrom(s => s.PhoneType));

            CreateMap<ResponsibleRequestDto, Responsible>()
                .ForPath(c => c.Cpf, opt => opt.MapFrom(s => s.Cpf))
                .ForPath(c => c.Email, opt => opt.MapFrom(s => s.Email))
                .ForPath(c => c.KinshipType, opt => opt.MapFrom(s => s.KinshipType));
                //.ForPath(c => c.GenderType, opt => opt.MapFrom(s => s.GenderType));

        }
    }
}
