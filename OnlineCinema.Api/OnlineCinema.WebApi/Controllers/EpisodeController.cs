using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с эпизодами сериала.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : Controller
    {
        private readonly IEpisodeService _episodeService;

        public EpisodeController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        /// <summary>
        /// Получение списка эпизодов для заданного сезона
        /// </summary>
        /// <param name="seasonId">Идентификатор сезона</param>
        /// <returns>Список эпизодов</returns> 
        [HttpPost("list")]
        public async Task<IActionResult> GetEpisodes(Guid seasonId)
        {
            var episodesList = await _episodeService.GetEpisodes(seasonId);
            return Ok(episodesList);
        }

        /// <summary>
        /// Получение эпизода по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор эпизода</param>
        /// <returns>Эпизод DTO</returns>  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEpisodeById(Guid id)
        {
            var episode = await _episodeService.GetEpisodeById(id);
            return Ok(episode);
        }

        /// <summary>
        /// Создание эпизода
        /// </summary>
        /// <param name="episode">Данные эпизода</param>
        /// <returns>Созданный эпизод</returns>
        [HttpPost]
        public async Task<IActionResult> CreateEpisode([FromBody] ChangeEpisodeRequest episode)
        {
            await _episodeService.CreateEpisode(episode);
            return Ok(episode);
        }

        /// <summary>
        /// Обновление эпизода
        /// </summary>
        /// <param name="id">Идентификатор эпизода</param>
        /// <param name="episode">Данные эпизода</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEpisode(Guid id, [FromBody] ChangeEpisodeRequest episode)
        {
            await _episodeService.UpdateEpisode(id, episode);
            return Ok();
        }

        /// <summary>
        /// Удаление эпизода
        /// </summary>
        /// <param name="id">Идентификатор эпизода</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisode(Guid id)
        {
            var episode = await _episodeService.GetEpisodeById(id);
            if (episode == null)
            {
                return NotFound();
            }

            await _episodeService.DeleteEpisode(id);
            return Ok();
        }
    }
}