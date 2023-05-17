using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.GenreDtos;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    /// <summary>
    /// Реализация интерфейса сервиса для работы с жанрами.
    /// </summary>
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IOperationResponse _response;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса GenreService.
        /// </summary>
        /// <param name="genreRepository">Репозиторий жанров.</param>
        public GenreService(IGenreRepository genreRepository, IMapper mapper, IOperationResponse response)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _response = response;
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            var genreDtos = _mapper.Map<IEnumerable<GenreDto>>(genres);
            return _response.SuccessResponse(genreDtos);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> GetGenreByIdAsync(Guid genreId)
        {
            var genre = await _genreRepository.GetOrDefaultAsync(g => g.Id == genreId);
            if (genre is null)
                return _response.NotFound(new List<string> { $"Жанр с таким ID {genreId} не найден." });

            var genreDto = _mapper.Map<GenreDto>(genre);
            return _response.SuccessResponse(genreDto);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> CreateGenreAsync(GenreCreateDto model)
        {
            var genreExist = await _genreRepository.GetOrDefaultAsync(g => g.Name.ToUpper() == model.Name.ToUpper());
            if (genreExist is not null)
                return _response.BadRequest(new List<string> { $"Жанр с таким названием уже существует {model.Name}." }, model);

            var genre = _mapper.Map<DicGenreEntity>(model);
            await _genreRepository.AddAsync(genre);
            var createdGenre = await _genreRepository.GetOrDefaultAsync(g => g.Name == model.Name);
            return _response.CreatedSuccessfully(createdGenre!.Id);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> UpdateGenreAsync(GenreUpdateDto model)
        {
            var genre = await _genreRepository.GetOrDefaultAsync(g => g.Id == model.Id);
            if (genre is null)
                return _response.NotFound(new List<string> { $"Жанр с таким ID: {model.Id} не найден." }, model);

            var genreNameExist = await _genreRepository.GetOrDefaultAsync(g => g.Name.ToUpper() == model.Name.ToUpper() && 
                                                                               g.Id != model.Id);
            if (genreNameExist is not null)
                return _response.BadRequest(new List<string> { $"Жанр с таким названием уже существует {model.Name}." }, model);

            genre = _mapper.Map<DicGenreEntity>(model);
            await _genreRepository.UpdateAsync(genre);
            return _response.UpdatedSuccessfully();
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> DeleteGenreAsync(Guid genreId)
        {
            var genreExist = await _genreRepository.GetOrDefaultAsync(g => g.Id == genreId);
            if (genreExist is null)
                return _response.NotFound(new List<string> { $"Жанр с таким ID: {genreId} не найден." });

            await _genreRepository.DeleteAsync(genreExist);
            return _response.DeleteSuccessfully();
        }

        /// <inheritdoc/>
        public ResponseDto ModelIsNotValid(ModelStateDictionary modelState) => _response.ModelStateIsNotValid(modelState);
    }
}
