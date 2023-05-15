using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OnlineCinema.Data;
using OnlineCinema.Data.Repositories;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Mapper;
using OnlineCinema.Logic.Services;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Tests
{
    [TestFixture]
    public class MovieServiceTests
    {
        static DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "OnlineCinema.Api.Db")
            .Options;

        private ApplicationDbContext _appDbContext;
        private IMovieService _movieService;
        private IMapper _mapper;
        private readonly ILogger<MovieService> _logger;
        private MovieRepository _movieRepository;
        
        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>());
            _mapper = new Mapper(config);
            _appDbContext = new ApplicationDbContext(options);
            _appDbContext.Database.EnsureDeleted();
            _movieRepository = new MovieRepository(_appDbContext);
            _movieService = new MovieService(_mapper, _logger, _movieRepository);
        }

        [Test]
        public async Task GetMovies_ShouldReturnAllMovie()
        {
            await _movieService.CreateMovie(new ChangeMovieRequest
            {
                Name = "Film One",
                DateAdded = DateTime.Now,
                ReleaseYear = DateTime.Now,
                MoviePosterUrl = "//MoviePosterUrl",
                IsSeries = false,
                ContentUrl = "//ContentUrl"
            });
            
            await _movieService.CreateMovie(new ChangeMovieRequest
            {
                Name = "Film Two",
                DateAdded = DateTime.Now,
                ReleaseYear = DateTime.Now,
                MoviePosterUrl = "//MoviePosterUrl",
                IsSeries = false,
                ContentUrl = "//ContentUrl"
            });
 
            var result = await _movieService.GetMovies(1, 10, new MovieFilter());
            Assert.That(result, Has.Count.EqualTo(2));
        }  
    }
}