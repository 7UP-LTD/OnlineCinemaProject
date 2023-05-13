using AutoMapper;
using OnlineCinema.Data.Entities;
using OnlineCinema.Logic.Dtos.AuthDtos;

namespace OnlineCinema.Logic.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<RegisterUserDto, UserEntity>()
                .ForMember(src => src.NormalizedEmail, opt => opt.MapFrom(dest => dest.Email.ToUpper()))
                .ForMember(src => src.NormalizedUserName, opt => opt.MapFrom(dest => dest.UserName.ToUpper()));
        }
    }
}
