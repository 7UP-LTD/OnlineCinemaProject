using System;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos.GenreDtos;
using OnlineCinema.Logic.Services.IServices;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с жанрами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        /// <summary>
        /// Конструктор класса GenreController.
        /// </summary>
        /// <param name="genreService">Сервис жанров.</param>
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Получение всех жанров.
        /// </summary>
        /// <returns>Список жанров.</returns>
        /// <response code="200">Успешный запрос. Возвращает коллекцию объектов жанров.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращает сообщение об ошибке.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenresAsync()
        {
            try
            {
                var result = await _genreService.GetAllGenresAsync();
                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования. Ещё бы вспомнить как один раз только делал.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Получение жанра по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор жанра.</param>
        /// <returns>Жанр с указанным идентификатором.</returns>
        /// <response code="200">Успешный ответ с кодом 200 OK и с данными о жанре.</response>
        /// <response code="404">Жанр не найден.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenreByIdAsync(Guid id)
        {
            try
            {
                var result = await _genreService.GetGenreByIdAsync(id);
                if (result.IsSuccess)
                    return Ok(result.Result);

                return NotFound(result.Errors);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования. Ещё бы вспомнить как один раз только делал.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Создание нового жанра.
        /// </summary>
        /// <param name="model">Модель данных для создания жанра.</param>
        /// <returns>Результат операции создания жанра.</returns>
        /// <response code="200">Успешный ответ с кодом 200 и c ID с ID созданного жанра.</response>
        /// <response code="404">Ошибка валидации модели. Возвращается BadRequestResponse с ошибками.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGenreAsync([FromBody] GenreCreateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList());

                var result = await _genreService.CreateGenreAsync(model);
                if (result.IsSuccess)
                    return Ok(result.Result);

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Обновление существующего жанра.
        /// </summary>
        /// <param name="model">Модель данных для обновления жанра.</param>
        /// <returns>Результат операции обновления жанра.</returns>
        /// <response code="200">Успешный ответ с кодом 200 жанр обновлен.</response>
        /// <response code="404">Жанр не найден с ответом об операции.</response>
        /// <response code="400">Некорректный запрос. С описанием ошибки.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGenreAsync([FromBody] GenreUpdateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList());

                var result = await _genreService.UpdateGenreAsync(model);
                if (result.IsSuccess)
                    return Ok();

                if (result.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(result.Errors);

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования. Ещё бы вспомнить как один раз только делал.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Удаляет жанр по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор жанра.</param>
        /// <returns>Результат операции удаления жанра.</returns>
        /// <response code="204">Успешный ответ с кодом 204 жанр удален.</response>
        /// <response code="404">Жанр не найден с ответом об операции.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGenreAsync(Guid id)
        {
            try
            {
                var result = await _genreService.DeleteGenreAsync(id);
                if (result.IsSuccess)
                    return NoContent();

                return NotFound(result.Errors);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования. Ещё бы вспомнить как один раз только делал.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
