using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<EpisodeService> _logger;
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodeService(IMapper mapper, ILogger<EpisodeService> logger, IEpisodeRepository episodeRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _episodeRepository = episodeRepository;
        }

        public async Task<List<MovieEpisodeDto>> GetEpisodes(Guid seasonId)
        {
            var episodes = await _episodeRepository.GetEpisodesBySeasonId(seasonId);
            return _mapper.Map<List<MovieEpisodeDto>>(episodes);
        }

        public async Task<MovieEpisodeDto> GetEpisodeById(Guid id)
        {
            var episode = await _episodeRepository.GetEpisodeById(id);
            if (episode == null)
            {
                _logger.LogError("Not found episode by id: {Id}", id);
                throw new ArgumentException("Not found");
            }

            return _mapper.Map<MovieEpisodeDto>(episode);
        }

        public async Task<Guid> CreateEpisode(ChangeEpisodeRequest episode)
        {
            var episodeEntity = _mapper.Map<MovieEpisodeEntity>(episode);
            episodeEntity.Id = Guid.NewGuid();
            episodeEntity.CreatedDate = DateTime.Now;
            await _episodeRepository.AddAsync(episodeEntity);
            return episodeEntity.Id;
        }

        public async Task UpdateEpisode(Guid id, ChangeEpisodeRequest episode)
        {
            var episodeEntity = await _episodeRepository.GetEpisodeById(id);
            _mapper.Map(episode, episodeEntity);
            await _episodeRepository.UpdateEpisode(id, episodeEntity);
        }

        public async Task DeleteEpisode(Guid id)
        {
            var episodeEntity = await _episodeRepository.GetEpisodeById(id);
            if (episodeEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _episodeRepository.DeleteAsync(episodeEntity);
        }
    }
}