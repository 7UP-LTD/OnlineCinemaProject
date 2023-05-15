using AutoMapper;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Filters;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.AuthDtos;
using OnlineCinema.Logic.Dtos.GenreDtos;
using OnlineCinema.Logic.Dtos.TagDtos;
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

            #region DicGenreEntity/GenreDto

            CreateMap<DicGenreEntity, GenreDto>()
                .ReverseMap();

            #endregion

            #region DicGenreEntity/GenreCreateDto

            CreateMap<GenreCreateDto, DicGenreEntity>()
                .ForMember(src => src.CreatedDate, opt => opt.MapFrom(dest => DateTime.Now));

            #endregion

            #region DicGenreEntity/GenreUpdateDto

            CreateMap<GenreUpdateDto, DicGenreEntity>().ReverseMap();

            #endregion

            #region DicTagEntity/TagDto

            CreateMap<DicTagEntity, TagDto>()
                .ReverseMap();

            #endregion

            #region DicTagEntity/TagCreateDto

            CreateMap<TagCreateDto, DicTagEntity>()
                .ForMember(src => src.CreatedDate, opt => opt.MapFrom(dest => DateTime.Now));

            #endregion
        }
    }
}
