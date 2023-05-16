using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineCinema.Data;
using OnlineCinema.Data.Repositories;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Mapper;
using OnlineCinema.Logic.Services;
using OnlineCinema.Logic.Services.IServices;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos.GenreDtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Data.Entities;

namespace OnlineCinema.Tests
{
    [TestFixture]
    public class GenreServiceTests
    {
        static DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "OnlineCinema.Api.Db")
            .Options;

        private ApplicationDbContext _appDbContext;
        private IGenreService _genreService;
        private IMapper _mapper;
        private readonly IOperationResponse _response;
        private GenreRepository _genreRepository;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>());
            _mapper = new Mapper(config);
            _appDbContext = new ApplicationDbContext(options);
            _appDbContext.Database.EnsureDeleted();
            _genreRepository = new GenreRepository(_appDbContext);
            _genreService = new GenreService(_genreRepository, _mapper, _response);
        }

        [Test]
        public async Task GetAllGenreAsync_ShouldReturnAllGenres()
        {
            await _genreService.CreateGenreAsync(new GenreCreateDto
            {
                Name = "Comedy"
            });

            await _genreService.CreateGenreAsync(new GenreCreateDto
            {
                Name = "Horror"
            });

            var result = await _genreService.GetAllGenresAsync();
            Assert.That(result, Has.Count.EqualTo(2));
        }

        //[Test]
        //public async Task CreateAndGetGenreAsync_ShouldReturnGenreById()
        //{
        //    var createResponse = await _genreService.CreateGenreAsync(new GenreCreateDto
        //    {
        //        Name = "Comedy"
        //    });

        //    var genreId = (Guid)createResponse.Result;

        //    var result = await _genreService.GetGenreByIdAsync(genreId);
        //    Assert.That(result, Is.Not.Null);
        //}

        //[Test]
        //public async Task Update_ShouldUpdateGenreById()
        //{
        //    var genreId = await _genreService.CreateGenreAsync(new GenreCreateDto
        //    {
        //        Name = "Comedy"
        //    });
        //    var genreDto = await _genreService.GetGenreByIdAsync(genreId);

        //    genreDto.Name = "Comedy Edited";
        //    var genre = _mapper.Map<GenreUpdateDto>(genreDto);
        //    await _genreService.UpdateGenreAsync(genre);

        //    var result = await _genreService.GetGenreByIdAsync(genreId);
        //    Assert.That(result.Name, Is.EqualTo("Genre Edited"));
        //}

        //[Test]
        //public async Task Delete_ShouldDeleteGenreById()
        //{
        //    var genreId = await _genreService.CreateGenreAsync(new GenreCreateDto
        //    {
        //        Name = "Comedy"
        //    });

        //    var result = await _genreService.GetGenreByIdAsync(genreId);
        //    Assert.That(result, Is.Not.Null);

        //    await _genreService.DeleteGenreAsync(genreId);

        //    ArgumentException? ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
        //    {
        //        result = await _genreService.GetGenreByIdAsync(genreId);
        //    });
        //    if (ex != null) Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'response')"));
        //}
    }
}
