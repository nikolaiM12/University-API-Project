using AutoMapper;
using OrganizationAPI.Domain.Abstractions.DTOs.Account;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.DTOs.User;

namespace OrganizationAPI.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountDto, RegisterDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();

            CreateMap<UserDto, RegisterDto>().ReverseMap();

            CreateMap<UserDto, AccountDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())                                                  
                .ReverseMap();
        }
    }
}
