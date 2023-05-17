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
    public class SeasonService : ISeasonService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SeasonService> _logger;
        private readonly ISeasonRepository _seasonRepository;

        public SeasonService(IMapper mapper, ILogger<SeasonService> logger, ISeasonRepository seasonRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _seasonRepository = seasonRepository;
        }


        public async Task<List<MovieSeasonDto>> GetSeasons(Guid movieId)
        {
            var seasons = await _seasonRepository.GetSeasonsByMovieId(movieId);
            return _mapper.Map<List<MovieSeasonDto>>(seasons);
        }

        public async Task<MovieSeasonDto> GetSeasonById(Guid id)
        {
            var season = await _seasonRepository.GetSeasonById(id);
            if (season == null)
            {
                _logger.LogError("Not found season by id: {Id}", id);
                throw new ArgumentException("Not found");
            }

            return _mapper.Map<MovieSeasonDto>(season);
        }

        public async Task<Guid> CreateSeason(ChangeSeasonRequest season)
        {
            var seasonEntity = _mapper.Map<MovieSeasonEntity>(season);
            seasonEntity.Id = Guid.NewGuid();
            seasonEntity.CreatedDate = DateTime.Now;
            await _seasonRepository.AddAsync(seasonEntity);
            return seasonEntity.Id;
        }

        public async Task UpdateSeason(Guid id, ChangeSeasonRequest season)
        {
            var seasonEntity = await _seasonRepository.GetSeasonById(id);
            _mapper.Map(season, seasonEntity);
            await _seasonRepository.UpdateSeason(id, seasonEntity);
        }

        public async Task DeleteSeason(Guid id)
        {
            var seasonEntity = await _seasonRepository.GetSeasonById(id);
            if (seasonEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _seasonRepository.DeleteAsync(seasonEntity);
        }
    }
}