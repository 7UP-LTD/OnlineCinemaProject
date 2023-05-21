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
        private readonly IMovieGenreRepository _movieGenreRepository;
        private readonly IMovieTagRepository _movieTagRepository;
        private readonly ITagService _tagService;

        public MovieService(IMapper mapper, ILogger<MovieService> logger, IMovieRepository movieRepository,
            ITagService tagService, IMovieTagRepository movieTagRepository, IMovieGenreRepository movieGenreRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _movieRepository = movieRepository;
            _tagService = tagService;
            _movieTagRepository = movieTagRepository;
            _movieGenreRepository = movieGenreRepository;
        }

        public async Task<List<MovieDto>> GetMovies(int page, int pageSize, MovieFilter? filter)
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
            var listOfTagsGuid = await GetTagsGuid(movie);
            movieEntity.Tags.Clear();
            foreach (var tagGuid in listOfTagsGuid)
            {
                movieEntity.Tags.Add(new MovieTagEntity
                {
                    Id = Guid.NewGuid(),
                    TagId = tagGuid,
                    MovieId = movieEntity.Id
                });
            }

            var listOfGenres = movie.Genres;
            movieEntity.Genres.Clear();
            foreach (var genreGuid in listOfGenres)
            {
                movieEntity.Genres.Add(new MovieGenreEntity
                {
                    Id = Guid.NewGuid(),
                    DicGenreId = genreGuid,
                    MovieId = movieEntity.Id
                });
            }

            await _movieRepository.AddAsync(movieEntity);
            return movieEntity.Id;
        }

        private async Task<List<Guid>> GetTagsGuid(ChangeMovieRequest movie)
        {
            var listOfTags = movie.Tags;
            var listOfTagsGuid = new List<Guid>();
            foreach (var tag in listOfTags)
            {
                Guid? tagEntityId = null;
                var responseResult = await _tagService.GetTagByNameAsync(tag);
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

            return listOfTagsGuid;
        }


        public async Task UpdateMovie(Guid id, ChangeMovieRequest movie)
        {
            await UpdateMovieProperties(id, movie);
            await DeleteMovieCollections(id);
            await AddMovieCollections(id, movie);
        }

        private async Task AddMovieCollections(Guid id, ChangeMovieRequest movie)
        {
            var listOfTagsGuid = await GetTagsGuid(movie);
            foreach (var tagGuid in listOfTagsGuid)
            {
                await _movieTagRepository.AddAsync(new MovieTagEntity
                {
                    Id = Guid.NewGuid(),
                    TagId = tagGuid,
                    MovieId = id
                });
            }

            var listOfGenres = movie.Genres;
            foreach (var genreGuid in listOfGenres)
            {
                await _movieGenreRepository.AddAsync(new MovieGenreEntity
                {
                    Id = Guid.NewGuid(),
                    DicGenreId = genreGuid,
                    MovieId = id
                });
            }
        }

        private async Task UpdateMovieProperties(Guid id, ChangeMovieRequest movie)
        {
            var movieEntity = await _movieRepository.GetMovieById(id);
            _mapper.Map(movie, movieEntity);
            await _movieRepository.UpdateAsync(movieEntity);
        }

        private async Task DeleteMovieCollections(Guid id)
        {
            var genres = await _movieGenreRepository
                .GetAllAsync(x => x.MovieId == id, tracked: true);
            foreach (var genre in genres)
            {
                await _movieGenreRepository.DeleteAsync(genre);
            }

            var tags = await _movieTagRepository
                .GetAllAsync(x => x.MovieId == id, tracked: true);
            foreach (var tag in tags)
            {
                await _movieTagRepository.DeleteAsync(tag);
            }
        }

        public async Task DeleteMovie(Guid id)
        {
            var movieEntity = await _movieRepository.GetMovieById(id);
            if (movieEntity == null)
            {
                _logger.LogError("Not found movie by id: {Id}", id);
                throw new ArgumentException("Not found");
            }

            await _movieRepository.DeleteAsync(movieEntity);
        }
    }
}