
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.TagDtos;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Services.IServices;
using Org.BouncyCastle.Pkcs;
using System.Net;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с тегами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IErrorResponse _errorResponse;

        /// <summary>
        /// Конструктор контроллера тегов.
        /// </summary>
        /// <param name="tagService">Сервис тегов.</param>
        /// <param name="errorResponse">Сервис для формирования ошибочных ответов.</param>
        public TagController(ITagService tagService, IErrorResponse errorResponse)
        {
            _tagService = tagService;
            _errorResponse = errorResponse;
        }

        /// <summary>
        /// Получить список всех тегов.
        /// </summary>
        /// <returns>Список всех тегов.</returns>
        /// <response code="200">Успешный запрос. Возвращает коллекцию объектов тегов.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращает сообщение об ошибке.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTagsAsync()
        {
            try
            {
                var result = await _tagService.GetAllTagsAsync();
                return Ok(result);
            }
            catch
            {
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }

        /// <summary>
        /// Получение тега по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор тега.</param>
        /// <returns>Тег с указанным идентификатором.</returns>
        /// <response code="200">Успешный ответ с кодом 200 OK и с данными о теге.</response>
        /// <response code="404">Тег не найден.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTagBuIdAsync(Guid id)
        {
            try
            {
                var result = await _tagService.GetTagByIdAsync(id);
                if (result.IsSuccess)
                    return Ok(result);

                return NotFound(result);
            }
            catch
            {
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }

        /// <summary>
        /// Создание нового тега.
        /// </summary>
        /// <param name="model">Модель данных для создания тега.</param>
        /// <returns>Результат операции создания тега.</returns>
        /// <response code="200">Успешный ответ с кодом 200 и с ответом о создании.</response>
        /// <response code="404">Ошибка валидации модели. Возвращается BadRequestResponse с ошибками.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTagAsync([FromBody] TagCreateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(_tagService.ModelStateIsValid(ModelState));

                var result = await _tagService.CreateTagAsync(model);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch
            {
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }

        /// <summary>
        /// Обновление существующего тега.
        /// </summary>
        /// <param name="model">Модель данных для обновления тега.</param>
        /// <returns>Результат операции обновления тега.</returns>
        /// <response code="200">Успешный ответ с кодом 200 тег обновлен.</response>
        /// <response code="404">Тег не найден с ответом об операции.</response>
        /// <response code="400">Некорректный запрос. С описанием ошибки.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response> 
        [HttpPut]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTagAsync([FromBody] TagDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(_tagService.ModelStateIsValid(ModelState));

                var result = await _tagService.UpdateTagAsync(model);
                if (result.IsSuccess)
                    return Ok(result);

                if (result.StatusCode == HttpStatusCode.NotFound) 
                    return NotFound(result);

                return BadRequest(result);
            }
            catch
            {
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }

        /// <summary>
        /// Удаляет тег по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор тега.</param>
        /// <returns>Результат операции удаления тега.</returns>
        /// <response code="204">Успешный ответ с кодом 204 тег удален.</response>
        /// <response code="404">Тег не найден с ответом об операции.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTagAsync(Guid id)
        {
            try
            {
                var result = await _tagService.DeleteTagAsync(id);
                if (result.IsSuccess)
                    return Ok(result);

                return NotFound(result);
            }
            catch
            {
                var errorModel = _errorResponse.InternalServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }
    }
}
