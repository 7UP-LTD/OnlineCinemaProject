﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.TagDtos;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    /// <summary>
    /// Реализация интервейса сервиса для работы с тегами.
    /// </summary>
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly IOperationResponse _response;

        /// <summary>
        /// Конструктор сервиса тегов.
        /// </summary>
        /// <param name="tagRepository">Репозиторий тегов.</param>
        /// <param name="mapper">Маппер для преобразования объектов.</param>
        /// <param name="response">Сервис для формирования ответов.</param>
        public TagService(ITagRepository tagRepository, IMapper mapper, IOperationResponse response)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _response = response;
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> GetAllTagsAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            var tagDtos = _mapper.Map<IEnumerable<TagDto>>(tags);
            return _response.SuccessResponse(tagDtos);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> GetTagByIdAsync(Guid tagId)
        {
            var tag = await _tagRepository.GetOrDefaultAsync(t => t.Id == tagId);
            if (tag is null)
                return _response.NotFound($"Тег с таким ID {tagId} не найден.");

            var tagDto = _mapper.Map<TagDto>(tag);
            return _response.SuccessResponse(tagDto);
        }
        
        /// <inheritdoc/>
        public async Task<ResponseDto> GetTagByNameAsync(string tagName)
        {
            var tag = await _tagRepository.GetOrDefaultAsync(t => t.Name.ToUpper() == tagName.ToUpper());
            if (tag is null)
                return _response.NotFound($"Тег с таким ID {tagName} не найден.");

            var tagDto = _mapper.Map<TagDto>(tag);
            return _response.SuccessResponse(tagDto);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> CreateTagAsync(TagCreateDto model)
        {
            var tagExist = await _tagRepository.GetOrDefaultAsync(t => t.Name.ToUpper() == model.Name.ToUpper());
            if (tagExist is not null)
                return _response.BadRequest($"Тег с таким наименование уже существует {model.Name}.");

            var tag = _mapper.Map<DicTagEntity>(model);
            await _tagRepository.AddAsync(tag);
            return _response.CreatedSuccessfully(tag!.Id);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> UpdateTagAsync(TagDto model)
        {
            var tag = await _tagRepository.GetOrDefaultAsync(t => t.Id == model.Id);
            if (tag is null)
                return _response.NotFound("Тег не найден.");

            var isNameExist = await _tagRepository.GetOrDefaultAsync(t => t.Name.ToUpper() == model.Name.ToUpper() &&
                                                                          t.Id != model.Id);
            if (isNameExist is not null)
                return _response.BadRequest($"Тег с таким наименование {model.Name} уже существует.");

            tag = _mapper.Map<DicTagEntity>(model);
            await _tagRepository.UpdateAsync(tag);
            return _response.UpdatedSuccessfully();
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> DeleteTagAsync(Guid tagId)
        {
            var tag = await _tagRepository.GetOrDefaultAsync(t => t.Id == tagId);
            if (tag is null)
                return _response.NotFound("Тег с таким ID не найден.");

            await _tagRepository.DeleteAsync(tag);
            return _response.DeleteSuccessfully();
        }
    }
}
