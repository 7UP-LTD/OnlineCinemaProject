using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineCinema.Data.Filters;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MovieService> _logger;
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMapper mapper, ILogger<MovieService> logger, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _movieRepository = movieRepository;
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
            var movieEntity = _mapper.Map<MovieEntityFilter>(movie);
            
        }

        public async Task UpdateMovie(Guid id, ChangeMovieRequest movie)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMovie(Guid id)
        {
            var movieEntity = await _movieRepository.GetMovieById(id);
            if (movieEntity == null)
            {
                _logger.LogError("Not found habit by id: {Id}", id);
                throw new ArgumentException("Not found");
            }

            _movieRepository.DeleteAsync(movieEntity);
        }
    }
}