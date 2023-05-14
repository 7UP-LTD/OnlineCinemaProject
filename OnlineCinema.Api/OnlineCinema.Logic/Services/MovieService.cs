using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineCinema.Data;
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

        public MovieService(IMapper mapper, ILogger<MovieService> logger)
        {
            _mapper = mapper;
            _logger = logger;
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

        public Guid CreateMovie(ChangeMovieRequest movie, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateMovie(Guid id, ChangeMovieRequest movie)
        {
            throw new NotImplementedException();
        }

        public void DeleteMovie(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}