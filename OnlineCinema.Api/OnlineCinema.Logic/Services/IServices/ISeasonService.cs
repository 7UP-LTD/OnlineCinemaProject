using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineCinema.Logic.Dtos.MovieDtos;

namespace OnlineCinema.Logic.Services.IServices
{
    public interface ISeasonService
    {
        /// <summary>
        /// Получение списка сезонов для заданного сериала
        /// </summary>
        /// <param name="movieId">Идентификатор сериала</param>
        /// <returns>Список сезонов</returns>
        Task<List<MovieSeasonDto>> GetSeasons(Guid movieId);

        /// <summary>
        /// Получение сезона по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сезона</param>
        /// <returns>Сезон DTO</returns>
        Task<MovieSeasonDto> GetSeasonById(Guid id);

        /// <summary>
        /// Создание сезона
        /// </summary>
        /// <param name="season">Данные сезона</param>
        /// <returns>Идентификатор созданного сезона</returns>
        Task<Guid> CreateSeason(ChangeSeasonRequest season);

        /// <summary>
        /// Обновление сезона
        /// </summary>
        /// <param name="id">Идентификатор сезона</param>
        /// <param name="season">Данные сезона</param>
        Task UpdateSeason(Guid id, ChangeSeasonRequest season);

        /// <summary>
        /// Удаление сезона
        /// </summary>
        /// <param name="id">Идентификатор сезона</param>
        Task DeleteSeason(Guid id);
    }
}