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
using OnlineCinema.Logic.Dtos.TagDtos;
using OnlineCinema.Logic.Mapper;
using OnlineCinema.Logic.Response.IResponse;
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
        private TagRepository _tagRepository;
        private TagService _tagService;
        private readonly IOperationResponse _response;
        private IMovieTagRepository _movieTagRepository;
        private IMovieGenreRepository _movieGenreRepository;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>());
            _mapper = new Mapper(config);
            _appDbContext = new ApplicationDbContext(options);
            _appDbContext.Database.EnsureDeleted();
            _movieRepository = new MovieRepository(_appDbContext);
            _tagRepository = new TagRepository(_appDbContext);
            _movieService = new MovieService(_mapper, _logger, _movieRepository,
                _tagService, _movieTagRepository, _movieGenreRepository);

            _tagService = new TagService(_tagRepository, _mapper, _response);
        }

        [Test]
        public async Task GetMovies_ShouldReturnAllMovie()
        {
            await _movieService.CreateMovie(new ChangeMovieRequest
            {
                Name = "Film One",
                ReleaseDate = DateTime.Now,
                MoviePosterUrl = "//MoviePosterUrl",
                IsSeries = false,
                ContentUrl = "//ContentUrl"
            });

            await _movieService.CreateMovie(new ChangeMovieRequest
            {
                Name = "Film Two",
                ReleaseDate = DateTime.Now,
                MoviePosterUrl = "//MoviePosterUrl",
                IsSeries = false,
                ContentUrl = "//ContentUrl"
            });

            var result = await _movieService.GetMovies(1, 10, new MovieFilter());
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task CreateAndGetMovie_ShouldReturnMovieById()
        {
            var movieId = await _movieService.CreateMovie(new ChangeMovieRequest
            {
                Name = "Film One",
                ReleaseDate = DateTime.Now,
                MoviePosterUrl = "//MoviePosterUrl",
                IsSeries = false,
                ContentUrl = "//ContentUrl"
            });

            var result = await _movieService.GetMovieById(movieId);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Update_ShouldUpdateMovieById()
        {
            var movieId = await _movieService.CreateMovie(new ChangeMovieRequest
            {
                Name = "Film One",
                ReleaseDate = DateTime.Now,
                MoviePosterUrl = "//MoviePosterUrl",
                IsSeries = false,
                ContentUrl = "//ContentUrl"
            });
            var movieDto = await _movieService.GetMovieById(movieId);

            movieDto.Name = "Film Edited";
            var movie = _mapper.Map<ChangeMovieRequest>(movieDto);
            await _movieService.UpdateMovie(movieId, movie);

            var result = await _movieService.GetMovieById(movieId);
            Assert.That(result.Name, Is.EqualTo("Film Edited"));
        }

        [Test]
        public async Task Delete_ShouldDeleteMovieById()
        {
            var movieId = await _movieService.CreateMovie(new ChangeMovieRequest
            {
                Name = "Film One",
                ReleaseDate = DateTime.Now,
                MoviePosterUrl = "//MoviePosterUrl",
                IsSeries = false,
                ContentUrl = "//ContentUrl"
            });

            var result = await _movieService.GetMovieById(movieId);
            Assert.That(result, Is.Not.Null);

            await _movieService.DeleteMovie(movieId);

            ArgumentException? ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                result = await _movieService.GetMovieById(movieId);
            });
            if (ex != null) Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'logger')"));
        }

        [Test]
        public async Task UpdateWithTag_ShouldUpdateMovieById()
        {
            var movieId = await _movieService.CreateMovie(new ChangeMovieRequest
            {
                Name = "Film One",
                ReleaseDate = DateTime.Now,
                MoviePosterUrl = "//MoviePosterUrl",
                IsSeries = false,
                ContentUrl = "//ContentUrl"
            });


            var movieDto = await _movieService.GetMovieById(movieId);

            movieDto.Name = "Film Edited";
            var movie = _mapper.Map<ChangeMovieRequest>(movieDto);
            await _movieService.UpdateMovie(movieId, movie);

            var result = await _movieService.GetMovieById(movieId);
            Assert.That(result.Name, Is.EqualTo("Film Edited"));
        }
    }
}