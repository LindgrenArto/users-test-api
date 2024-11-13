using AutoMapper;
using UsersTestApi.DTOModels;
using UsersTestApi.Models;

namespace UsersTestApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Map User to UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));

            // Map Address to AddressDTO
            CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.Geo, opt => opt.MapFrom(src => src.Geo));

            // Map Geo to GeoDTO
            CreateMap<Geo, GeoDTO>();

            // Map Company to CompanyDTO
            CreateMap<Company, CompanyDTO>();

            // Reverse mapping
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));

            CreateMap<AddressDTO, Address>()
                .ForMember(dest => dest.Geo, opt => opt.MapFrom(src => src.Geo));

            CreateMap<GeoDTO, Geo>();
            CreateMap<CompanyDTO, Company>();
        }
    }
}
