﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Filters;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.BlobDtos;
using OnlineCinema.Logic.Dtos.GenreDtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Dtos.MovieDtos.MainPageDtos;
using OnlineCinema.Logic.Dtos.TagDtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MovieService> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieGenreRepository _movieGenreRepository;
        private readonly IMovieTagRepository _movieTagRepository;
        private readonly ITagService _tagService;
        private readonly IBlobService _blobService;
        private readonly IGenreRepository _genreRepository;


        public MovieService(IMapper mapper, ILogger<MovieService> logger, IMovieRepository movieRepository,
            ITagService tagService, IMovieTagRepository movieTagRepository, IMovieGenreRepository movieGenreRepository,
            IBlobService blobService, IGenreRepository genreRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _movieRepository = movieRepository;
            _tagService = tagService;
            _movieTagRepository = movieTagRepository;
            _movieGenreRepository = movieGenreRepository;
            _blobService = blobService;
            _genreRepository = genreRepository;
        }

        public async Task<List<MovieDto>> GetMovies(int page, int pageSize, MovieFilter? filter)
        {
            var filterEntity = _mapper.Map<MovieEntityFilter>(filter);
            var movies = await _movieRepository.GetPagedMovies(page, pageSize, filterEntity);
            return _mapper.Map<List<MovieDto>>(movies);
        }

        public async Task<MovieMainView> GetMoviesForMain(Guid? userId)
        {
            var numberOfMovie = 5;
            var mainModelView = new MovieMainView();
            var filter = new MovieEntityFilter();
            filter.IsSeries = false;

            var newMovies = await _movieRepository.GetPagedMovies(1, numberOfMovie, filter);
            mainModelView.NewMovies = _mapper.Map<List<MovieView>>(newMovies);

            var topMovies = await _movieRepository.GetTopMovies(numberOfMovie, null);
            mainModelView.TopMovies = _mapper.Map<List<MovieView>>(topMovies);
            mainModelView.Banners = topMovies.Select(b => b.MovieBannerUrl).ToList();

            if (userId != null)
            {
                var recommendedMovies = await _movieRepository.GetTopUserMovies(userId, numberOfMovie);
                mainModelView.recommendedMovies = _mapper.Map<List<MovieView>>(recommendedMovies);
            }

            var useGenres = await _movieRepository.GetTopGenres(numberOfMovie);
            foreach (var genreId in useGenres)
            {
                if (filter.Genres != null)
                {
                    filter.Genres.Clear();
                    filter.Genres?.Add(genreId);
                }

                var movies = await _movieRepository.GetPagedMovies(1, numberOfMovie, filter);
                var moviesView = new GenreMovies();
                moviesView.Movies = _mapper.Map<List<MovieView>>(movies);
                var genre = await _genreRepository.GetOrDefaultAsync(x => x.Id == genreId);
                moviesView.Genre = _mapper.Map<GenreDto>(genre);
                var listMovies = new List<GenreMovies>();
                listMovies.Add(moviesView);
                mainModelView.GenreMovies = listMovies;
            }

            return mainModelView;
        }

        public async Task<MovieDto> GetMovieById(Guid id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                _logger.LogError("Not found movie by id: {Id}", id);
                throw new ArgumentException("Not found");
            }

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<Guid> CreateMovie(ChangeMovieRequest movie)
        {
            var movieEntity = _mapper.Map<MovieEntity>(movie);
            movieEntity.Id = Guid.NewGuid();
            movieEntity.CreatedDate = DateTime.Now;

            await UploadFile(movie, movieEntity);

            var listOfTagsGuid = await GetTagsGuid(movie);
            movieEntity.Tags.Clear();
            foreach (var tagGuid in listOfTagsGuid)
            {
                movieEntity.Tags.Add(new MovieTagEntity
                {
                    Id = Guid.NewGuid(),
                    TagId = tagGuid,
                    MovieId = movieEntity.Id
                });
            }

            var listOfGenres = movie.Genres;
            movieEntity.Genres.Clear();
            foreach (var genreGuid in listOfGenres)
            {
                movieEntity.Genres.Add(new MovieGenreEntity
                {
                    Id = Guid.NewGuid(),
                    DicGenreId = genreGuid,
                    MovieId = movieEntity.Id
                });
            }

            await _movieRepository.AddAsync(movieEntity);
            return movieEntity.Id;
        }

        private async Task<List<Guid>> GetTagsGuid(ChangeMovieRequest movie)
        {
            var listOfTags = movie.Tags;
            var listOfTagsGuid = new List<Guid>();
            foreach (var tag in listOfTags)
            {
                Guid? tagEntityId = null;
                var responseResult = await _tagService.GetTagByNameAsync(tag);
                if (responseResult.IsSuccess)
                {
                    tagEntityId = ((TagDto) responseResult.Result!).Id;
                }
                else
                {
                    var createResponse = await _tagService.CreateTagAsync(new TagCreateDto
                    {
                        Name = tag
                    });
                    if (createResponse.IsSuccess)
                    {
                        tagEntityId = (Guid) createResponse.Result!;
                    }
                }

                if (tagEntityId.HasValue)
                {
                    listOfTagsGuid.Add(tagEntityId.Value);
                }
            }

            return listOfTagsGuid;
        }


        public async Task UpdateMovie(Guid id, ChangeMovieRequest movie)
        {
            await UpdateMovieProperties(id, movie);
            await DeleteMovieCollections(id);
            await AddMovieCollections(id, movie);
        }

        private async Task AddMovieCollections(Guid id, ChangeMovieRequest movie)
        {
            var listOfTagsGuid = await GetTagsGuid(movie);
            foreach (var tagGuid in listOfTagsGuid)
            {
                await _movieTagRepository.AddAsync(new MovieTagEntity
                {
                    Id = Guid.NewGuid(),
                    TagId = tagGuid,
                    MovieId = id
                });
            }

            var listOfGenres = movie.Genres;
            foreach (var genreGuid in listOfGenres)
            {
                await _movieGenreRepository.AddAsync(new MovieGenreEntity
                {
                    Id = Guid.NewGuid(),
                    DicGenreId = genreGuid,
                    MovieId = id
                });
            }
        }

        private async Task UpdateMovieProperties(Guid id, ChangeMovieRequest movie)
        {
            var movieEntity = await _movieRepository.GetMovieById(id);
            _mapper.Map(movie, movieEntity);
            if (movieEntity != null)
            {
                await UploadFile(movie, movieEntity);
                await _movieRepository.UpdateAsync(movieEntity);
            }
        }

        private async Task UploadFile(ChangeMovieRequest movie, MovieEntity? movieEntity)
        {
            if (movie.filePoster != null)
            {
                BlobResponseDto blobResponsePoster = await _blobService.UploadFileAsync(movie.filePoster);
                if (blobResponsePoster.IsSuccess)
                {
                    if (!movieEntity.MoviePosterUrl.IsNullOrEmpty())
                    {
                        await _blobService.DeleteFileAsync(movieEntity.MoviePosterUrl);
                    }

                    movieEntity.MoviePosterUrl = blobResponsePoster.Url;
                }
            }

            if (movie.fileBanner != null)
            {
                BlobResponseDto blobResponseBanner = await _blobService.UploadFileAsync(movie.fileBanner);

                if (blobResponseBanner.IsSuccess)
                {
                    if (!movieEntity.MovieBannerUrl.IsNullOrEmpty())
                    {
                        await _blobService.DeleteFileAsync(movieEntity.MovieBannerUrl);
                    }

                    movieEntity.MovieBannerUrl = blobResponseBanner.Url;
                }
            }
        }


        private async Task DeleteMovieCollections(Guid id)
        {
            var genres = await _movieGenreRepository
                .GetAllAsync(x => x.MovieId == id, tracked: true);
            foreach (var genre in genres)
            {
                await _movieGenreRepository.DeleteAsync(genre);
            }

            var tags = await _movieTagRepository
                .GetAllAsync(x => x.MovieId == id, tracked: true);
            foreach (var tag in tags)
            {
                await _movieTagRepository.DeleteAsync(tag);
            }
        }

        public async Task DeleteMovie(Guid id)
        {
            var movieEntity = await _movieRepository.GetMovieById(id);
            if (movieEntity == null)
            {
                _logger.LogError("Not found movie by id: {Id}", id);
                throw new ArgumentException("Not found");
            }

            if (!movieEntity.MovieBannerUrl.IsNullOrEmpty())
            {
                await _blobService.DeleteFileAsync(movieEntity.MovieBannerUrl);
            }

            if (!movieEntity.MoviePosterUrl.IsNullOrEmpty())
            {
                await _blobService.DeleteFileAsync(movieEntity.MoviePosterUrl);
            }

            await _movieRepository.DeleteAsync(movieEntity);
        }
    }
}