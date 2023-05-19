using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos.CommentDto;
using OnlineCinema.Logic.Dtos.CommentDtos;
using OnlineCinema.Logic.Services.IServices;
using System.Net;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для управления комментариями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        /// <summary>
        /// Конструктор класса контроллера для упарвления комментариями.
        /// </summary>
        /// <param name="commentService"></param>
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Создает новый комментарий для фильма.
        /// </summary>
        /// <param name="model">Модель для создания нового комментария для фильмы. См. <see cref="NewCommentDto"/>.</param>
        /// <returns>Возвращает ID созданного комментария со статусом 200 или в статус об ошибке с описанием ошибки.</returns>
        /// <response code="200">Успешный ответ с кодом 200 и ID созданного комментария.</response>
        /// <response code="400">Некорректный запрос. Ответ со статусом 400 и со списком ошибок.</response>
        /// <response code="404">Не найден фильм или пользователь по указаным ID. Статус ответа 404 со списком ошибок.</response>
        /// <response code="500">Внутренняя ошибка сервера. Ответ со статусом 500 и описанием ошибки.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNewCommnetAsync(NewCommentDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList());

                var operationResult = await _commentService.PostNewCommentAsync(model);
                if (operationResult.IsSuccess)
                    return Ok(operationResult.Result);

                if (operationResult.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(operationResult.Errors);

                return BadRequest(operationResult.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Обновить текст комментария.
        /// </summary>
        /// <param name="model">Модель для обновления текста комментария <see cref="UpdateCommnetDto"/>.</param>
        /// <returns></returns>
        /// <response code="200">Успешный ответ с кодом 200 и ID обновленного комментария.</response>
        /// <response code="400">Некорректный запрос. Ответ со статусом 400 и со списком ошибок.</response>
        /// <response code="404">Не найден фильм или пользователь по указаным ID. Статус ответа 404 со списком ошибок.</response>
        /// <response code="500">Внутренняя ошибка сервера. Ответ со статусом 500 и описанием ошибки.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCommentAsync(UpdateCommnetDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList());

                var operationResponse = await _commentService.UpdateCommentAsync(model);
                if (operationResponse.IsSuccess)
                    return Ok(operationResponse.Result);

                return NotFound(operationResponse.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Удалить комментарий.
        /// </summary>
        /// <param name="commentId">ID для кдаления комментария.</param>
        /// <returns></returns>
        /// <response code="200">Успешный ответ с кодом 204 нет контента.</response>
        /// <response code="404">Не найден комментарий по указанному ID. Статус ответа 404 со списком ошибок.</response>
        /// <response code="500">Внутренняя ошибка сервера. Ответ со статусом 500 и описанием ошибки.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCommentAsync(Guid commentId)
        {
            try
            {
                var operationResponse = await _commentService.DeleteCommentAsync(commentId);
                if (operationResponse.IsSuccess)
                    return NoContent();

                return NotFound(operationResponse.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
