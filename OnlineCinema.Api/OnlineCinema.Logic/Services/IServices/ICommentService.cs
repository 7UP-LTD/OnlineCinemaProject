using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.CommentDto;
using OnlineCinema.Logic.Dtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс для сервиса комментариев.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Опубликовать новый комментарий асинхронно.
        /// </summary>
        /// <param name="model">Модель нового комментария.</param>
        /// <returns>Ответ операции в виде объекта <see cref="ResponseDto"/>.</returns>
        Task<ResponseDto> PostNewCommentAsync(Guid userId, NewCommentDto model);

        /// <summary>
        /// Обновляет комментарий асинхронно.
        /// </summary>
        /// <param name="model">Модель обновляемого комментария.</param>
        /// <returns>Ответ операции в виде объекта <see cref="ResponseDto"/>.</returns>
        Task<ResponseDto> UpdateCommentAsync(UpdateCommnetDto model);

        /// <summary>
        /// Удаляет комментарий асинхронно.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <returns>Ответ операции в виде объекта <see cref="ResponseDto"/>.</returns>
        Task<ResponseDto> DeleteCommentAsync(Guid commentId);
    }
}
