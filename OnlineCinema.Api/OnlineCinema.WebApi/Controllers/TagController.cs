using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos.TagDtos;
using OnlineCinema.Logic.Services.IServices;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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

        /// <summary>
        /// Конструктор контроллера тегов.
        /// </summary>
        /// <param name="tagService">Сервис тегов.</param>
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        /// <summary>
        /// Получить список всех тегов.
        /// </summary>
        /// <returns>Список всех тегов.</returns>
        /// <response code="200">Успешный запрос. Возвращает коллекцию объектов тегов.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращает сообщение об ошибке.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TagDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTagsAsync()
        {
            try
            {
                var result = await _tagService.GetAllTagsAsync();
                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
        [ProducesResponseType(typeof(TagDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTagByIdAsync(Guid id)
        {
            try
            {
                var result = await _tagService.GetTagByIdAsync(id);
                if (result.IsSuccess)
                    return Ok(result.Result);

                return NotFound(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Создание нового тега.
        /// </summary>
        /// <param name="model">Модель данных для создания тега.</param>
        /// <returns>Результат операции создания тега.</returns>
        /// <response code="200">Успешный ответ с кодом 200 и с ответом о создании.</response>
        /// <response code="404">Ошибка валидации модели. Возвращается BadRequestResponse со списком ошибок.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTagAsync([FromBody] TagCreateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList());

                var result = await _tagService.CreateTagAsync(model);
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
        /// Обновление существующего тега.
        /// </summary>
        /// <param name="model">Модель данных для обновления тега.</param>
        /// <returns>Результат операции обновления тега.</returns>
        /// <response code="200">Успешный ответ с кодом 200 тег обновлен.</response>
        /// <response code="404">Тег не найден с ответом об операции.</response>
        /// <response code="400">Некорректный запрос. С описанием ошибки.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTagAsync([FromBody] TagDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList());

                var result = await _tagService.UpdateTagAsync(model);
                if (result.IsSuccess)
                    return Ok();

                if (result.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(result.Errors);

                return BadRequest(result.Errors);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Удаляет тег по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор тега.</param>
        /// <returns>Результат операции удаления тега.</returns>
        /// <response code="204">Успешный ответ с кодом 204 тег удален.</response>
        /// <response code="404">Тег не найден с ответом об операции и списком ошибок.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTagAsync(Guid id)
        {
            try
            {
                var result = await _tagService.DeleteTagAsync(id);
                if (result.IsSuccess)
                    return NoContent();

                return NotFound(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}