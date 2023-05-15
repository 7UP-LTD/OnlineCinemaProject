using AutoMapper;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Filters;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.AuthDtos;
using OnlineCinema.Logic.Dtos.MovieDtos;

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

            #region Movie

            CreateMap<MovieFilter, MovieEntityFilter>();
            CreateMap<ChangeMovieRequest, MovieEntity>();
            CreateMap<MovieDto, MovieEntity>().ReverseMap();
            CreateMap<MovieDto,ChangeMovieRequest>();
            #endregion
        }
    }
}