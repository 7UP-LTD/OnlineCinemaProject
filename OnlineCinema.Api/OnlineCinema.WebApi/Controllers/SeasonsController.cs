using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : Controller
    {
        private readonly ISeasonService _seasonService;

        public SeasonsController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        /// <summary>
        /// Получение списка сезонов фильма
        /// </summary>
        /// <param name="movieId">Идентификатор фильма</param>
        /// <returns>Список сезонов фильма</returns>
        [HttpGet]
        public async Task<IActionResult> GetSeasons(Guid movieId)
        {
            var seasonsList = await _seasonService.GetSeasons(movieId);
            return Ok(seasonsList);
        }

        /// <summary>
        /// Получение сезона по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сезона</param>
        /// <returns>Сезон DTO</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeasonById(Guid id)
        {
            var season = await _seasonService.GetSeasonById(id);
            return Ok(season);
        }
      
        /// <summary>
        /// Создание сезона
        /// </summary>
        /// <param name="season">Данные сезона</param>
        /// <returns>Созданный сезон</returns>
        [HttpPost]
        public async Task<IActionResult> CreateSeason([FromBody] ChangeSeasonRequest season)
        {
            var guid = await _seasonService.CreateSeason(season);
            return Ok(guid);
        }

        /// <summary>
        /// Обновление сезона
        /// </summary>
        /// <param name="id">Идентификатор сезона</param>
        /// <param name="season">Данные сезона</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeason(Guid id, [FromBody] ChangeSeasonRequest season)
        {
            await _seasonService.UpdateSeason(id, season);
            return Ok();
        }

        /// <summary>
        /// Удаление сезона
        /// </summary>
        /// <param name="id">Идентификатор сезона</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeason(Guid id)
        {
            var moviesItem = await _seasonService.GetSeasonById(id);
            if (moviesItem == null)
            {
                return NotFound();
            }

            await _seasonService.DeleteSeason(id);
            return Ok();
        }
    }
}