using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineCinema.Logic.Dtos.MovieDtos;

namespace OnlineCinema.Logic.Services.IServices
{
    public interface IEpisodeService
    {
        /// <summary>
        /// Получение списка эпизодов для заданного сезона
        /// </summary>
        /// <param name="seasonId">Идентификатор сезона</param>
        /// <returns>Список эпизодов</returns>
        Task<List<MovieEpisodeDto>> GetEpisodes(Guid seasonId);

        /// <summary>
        /// Получение эпизода по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор эпизода</param>
        /// <returns>Эпизод DTO</returns>
        Task<MovieEpisodeDto> GetEpisodeById(Guid id);

        /// <summary>
        /// Создание эпизода
        /// </summary>
        /// <param name="episode">Данные эпизода</param>
        /// <returns>Идентификатор созданного эпизода</returns>
        Task<Guid> CreateEpisode(ChangeEpisodeRequest episode);

        /// <summary>
        /// Обновление эпизода
        /// </summary>
        /// <param name="id">Идентификатор эпизода</param>
        /// <param name="episode">Данные эпизода</param>
        Task UpdateEpisode(Guid id, ChangeEpisodeRequest episode);

        /// <summary>
        /// Удаление эпизода
        /// </summary>
        /// <param name="id">Идентификатор эпизода</param>
        Task DeleteEpisode(Guid id);
    }
}