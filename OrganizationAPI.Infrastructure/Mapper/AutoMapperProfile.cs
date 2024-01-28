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
            /*CreateMap<RegisterDto, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())) // Създаване на ново Id
            .ForMember(dest => dest.AccountId, opt => opt.Ignore()) // Игнориране на AccountId, тъй като вероятно трябва да бъде зададено след регистрацията
            .ReverseMap();*/                                                         // Можете да добавите други нужни мапинги

            CreateMap<UserDto, AccountDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId)) // Map UserDto.Id към AccountDto.Id
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Игнориране на PasswordHash, тъй като вероятно трябва да бъде генерирано от AuthenticationService
                                                                           // Можете да добавите други нужни мапинги
                .ReverseMap();
        }
    }
}
