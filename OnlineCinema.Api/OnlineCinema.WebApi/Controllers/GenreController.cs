using System;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.GenreDtos;
using OnlineCinema.Logic.Response;
using OnlineCinema.Logic.Response.IResponse;
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
        private readonly IErrorResponse _errorResponse;

        /// <summary>
        /// Конструктор класса GenreController.
        /// </summary>
        /// <param name="genreService">Сервис жанров.</param>
        /// <param name="errorResponse">Объект для формирования ошибочного ответа.</param>
        public GenreController(IGenreService genreService, IErrorResponse errorResponse)
        {
            _genreService = genreService;
            _errorResponse = errorResponse;
        }

        /// <summary>
        /// Получение всех жанров.
        /// </summary>
        /// <returns>Список жанров.</returns>
        /// <response code="200">Успешный запрос. Возвращает коллекцию объектов жанров.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращает сообщение об ошибке.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenresAsync()
        {
            try
            {
                var result = await _genreService.GetAllGenresAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования. Ещё бы вспомнить как один раз только делал.
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }

        /// <summary>
        /// Получение жанра по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор жанра.</param>
        /// <returns>Жанр с указанным идентификатором.</returns>
        /// <response code="200">Успешный ответ с кодом 200 OK и с данными о жанре.</response>
        /// <response code="404">Жанр не найден.</response>
        /// <response code="400">Некорректный запрос. С описанием ошибки.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenreByIdAsync(Guid id)
        {
            try
            {
                var result = await _genreService.GetGenreByIdAsync(id);
                if (result.IsSuccess)
                    return Ok(result);

                if (result.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования. Ещё бы вспомнить как один раз только делал.
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }

        /// <summary>
        /// Создание нового жанра.
        /// </summary>
        /// <param name="model">Модель данных для создания жанра.</param>
        /// <returns>Результат операции создания жанра.</returns>
        /// <response code="200">Успешный ответ с кодом 200 и с ответом о создании.</response>
        /// <response code="404">Ошибка валидации модели. Возвращается BadRequestResponse с ошибками.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGenreAsync([FromBody] GenreCreateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(_genreService.ModelIsNotValid(ModelState));

                var result = await _genreService.CreateGenreAsync(model);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования. Ещё бы вспомнить как один раз только делал.
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
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
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGenreAsync([FromBody] GenreUpdateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(_genreService.ModelIsNotValid(ModelState));

                var result = await _genreService.UpdateGenreAsync(model);
                if (result.IsSuccess)
                    return Ok(result);

                if (result.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования. Ещё бы вспомнить как один раз только делал.
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
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
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGenreAsync(Guid id)
        {
            try
            {
                var result = await _genreService.DeleteGenreAsync(id);
                if (result.IsSuccess)
                    return NoContent();

                return NotFound(result);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования. Ещё бы вспомнить как один раз только делал.
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }
    }
}
