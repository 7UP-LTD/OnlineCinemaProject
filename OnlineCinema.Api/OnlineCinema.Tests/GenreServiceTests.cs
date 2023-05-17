using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineCinema.Data;
using OnlineCinema.Data.Repositories;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Mapper;
using OnlineCinema.Logic.Services;
using OnlineCinema.Logic.Services.IServices;
using OnlineCinema.Logic.Dtos.GenreDtos;
using System.Net;

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
        [TestCase("Comedy", "A genre of film that makes people laugh.")]
        [TestCase("Horror", "A genre of film that scares people.")]
        public async Task CreateGenreAsync_ShouldReturnSuccessfulResponse(string name, string description)
        {
            // Arrange
            var genreCreateDto = new GenreCreateDto
            {
                Name = name,
                Description = description
            };

            // Act
            var response = await _genreService.CreateGenreAsync(genreCreateDto);

            // Assert
            Assert.IsTrue(response.IsSuccess); // Check that the response is successful
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode); // Check that the status code is 201 (Created)
            Assert.IsInstanceOf<Guid>(response.Result); // Check that the result is a Guid
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

        [Test]
        public async Task CreateAndGetGenreAsync_ShouldReturnGenreById()
        {
            var createResponse = await _genreService.CreateGenreAsync(new GenreCreateDto
            {
                Name = "Comedy",
                Description = "A genre of film that makes people laugh."
            });

            Assert.That(createResponse.IsSuccess, Is.True);

            var result = await _genreService.GetGenreByIdAsync((Guid)createResponse.Result);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task UpdateGenreAsync_ShouldUpdateGenreName_WhenGenreExistsAndNameIsValid()
        {
            // Arrange
            var createResponse = await _genreService.CreateGenreAsync(new GenreCreateDto
            {
                Name = "Comedy",
                Description = "A genre of film that makes people laugh."
            });
            Assert.AreEqual(HttpStatusCode.OK, createResponse.StatusCode); // Check status code
            Assert.IsTrue(createResponse.IsSuccess); // Check success flag
            var responseDto = await _genreService.GetGenreByIdAsync((Guid)createResponse.Result);
            responseDto.Result = "Comedy Edited";
            var genreUpdateDto = _mapper.Map<GenreUpdateDto>(responseDto);

            // Act
            var updateResponse = await _genreService.UpdateGenreAsync(genreUpdateDto); // Assign update response to a variable
            Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode); // Check status code
            Assert.IsTrue(updateResponse.IsSuccess); // Check success flag
            var updatedGenreDto = await _genreService.GetGenreByIdAsync((Guid)createResponse.Result);

            // Assert
            Assert.AreEqual("Comedy Edited", updatedGenreDto.Result);
        }

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
