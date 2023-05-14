using AutoMapper;
using OnlineCinema.Data.Entities;
using OnlineCinema.Logic.Dtos.AuthDtos;

namespace OnlineCinema.Logic.Mapper
{
    /// <summary>
    /// Конфигурация маппера для преобразования объектов.
    /// </summary>
    public class MapperConfig : Profile
    {
        /// <summary>
        /// Конструктор конфигурации маппера.
        /// </summary>
        public MapperConfig()
        {
            #region RegisterUserDto/UserEntity

            CreateMap<RegisterUserDto, UserEntity>()
                .ForMember(src => src.NormalizedEmail, opt => opt.MapFrom(dest => dest.Email.ToUpper()))
                .ForMember(src => src.NormalizedUserName, opt => opt.MapFrom(dest => dest.UserName.ToUpper()));

            #endregion
        }
    }
}
