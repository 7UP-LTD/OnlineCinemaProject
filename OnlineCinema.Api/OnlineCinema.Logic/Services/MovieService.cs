using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Filters;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Dtos.TagDtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MovieService> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly ITagService _tagService;

        public MovieService(IMapper mapper, ILogger<MovieService> logger, IMovieRepository movieRepository,
            ITagService tagService)
        {
            _mapper = mapper;
            _logger = logger;
            _movieRepository = movieRepository;
            _tagService = tagService;
        }

        public async Task<List<MovieDto>> GetMovies(int page, int pageSize, MovieFilter? filter = null)
        {
            var filterEntity = _mapper.Map<MovieEntityFilter>(filter);
            var movies = await _movieRepository.GetPagedMovies(page, pageSize, filterEntity);
            return _mapper.Map<List<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieById(Guid id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                _logger.LogError("Not found movie by id: {Id}", id);
                throw new ArgumentException("Not found");
            }

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<Guid> CreateMovie(ChangeMovieRequest movie)
        {
            var movieEntity = _mapper.Map<MovieEntity>(movie);
            movieEntity.Id = Guid.NewGuid();
            movieEntity.CreatedDate = DateTime.Now;
            var listOfTags = movie.Tags;
            var listOfTagsGuid = new List<Guid>();
            foreach (var tag in listOfTags)
            {
                Guid? tagEntityId = null;
                var responseResult = await _tagService.GetTagByName(tag);
                if (responseResult.IsSuccess)
                {
                    tagEntityId = ((TagDto) responseResult.Result!).Id;
                }
                else
                {
                    var createResponse = await _tagService.CreateTagAsync(new TagCreateDto
                    {
                        Name = tag
                    });
                    if (createResponse.IsSuccess)
                    {
                        tagEntityId = (Guid) createResponse.Result!;
                    }
                }

                if (tagEntityId.HasValue)
                {
                    listOfTagsGuid.Add(tagEntityId.Value);
                }
            }

            foreach (var tagGuid in listOfTagsGuid)
            {
                movieEntity.Tags.Add(new MovieTagEntity
                {
                    Id = Guid.NewGuid(),
                    TagId = tagGuid,
                    MovieId = movieEntity.Id
                });
            }

            await _movieRepository.AddAsync(movieEntity);
            return movieEntity.Id;
        }


        public async Task UpdateMovie(Guid id, ChangeMovieRequest movie)
        {
            var movieEntity = await _movieRepository.GetMovieById(id);
            _mapper.Map(movie, movieEntity);
            await _movieRepository.UpdateMovie(id, movieEntity);
        }

        public async Task DeleteMovie(Guid id)
        {
            var movieEntity = await _movieRepository.GetMovieById(id);
            if (movieEntity == null)
            {
                // _logger.LogError("Not found movie by id: {Id}", id);
                throw new ArgumentException("Not found");
            }

            _movieRepository.DeleteAsync(movieEntity);
        }
    }
}