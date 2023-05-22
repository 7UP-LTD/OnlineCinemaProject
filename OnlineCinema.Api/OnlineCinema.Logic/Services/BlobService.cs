using System;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineCinema.Logic.Dtos.BlobDtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    /// <summary>
    /// Сервис для работы с хранилищем данных Asuze Blob.
    /// </summary>
    public class BlobService : IBlobService
    {
        private readonly BlobContainerClient _blobContainer;
        private readonly ILogger<BlobService> _logger;

        /// <summary>
        /// Класс, представляющий сервис работы с Blob Storage.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <param name="logger">Логгер.</param>
        public BlobService(IConfiguration configuration, ILogger<BlobService> logger)
        {
            _logger = logger;
            var credentinal = new StorageSharedKeyCredential(configuration["Azure:Account"], configuration["Azure:Key"]);
            var blobServiceClient = new BlobServiceClient(new Uri(configuration["Azure:BlobUri"]!), credentinal);
            _blobContainer = blobServiceClient.GetBlobContainerClient(configuration["Azure:Container"]);
        }

        /// <inheritdoc/>
        public async Task<BlobResponseDto> DeleteFileAsync(string fileUrl)
        {
            try
            {
                string imageName = Path.GetFileName(fileUrl);
                var blob = _blobContainer.GetBlobClient(imageName);
                var blobResponse = await blob.DeleteAsync();
                if (blobResponse.Status == 202)
                    return new BlobResponseDto { IsSuccess = true };

                return new BlobResponseDto
                {
                    IsSuccess = false,
                    Errors = $"Ошибка при удалении файла. Код состояния: {blobResponse.Status}, Описание: {blobResponse.ReasonPhrase}"
                };
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "Ошибка при удаление файла.");
                return new BlobResponseDto { IsSuccess = false };
            }
        }

        /// <inheritdoc/>
        public async Task<BlobResponseDto> UploadFileAsync(IFormFile file)
        {
            try
            {
                var imageExtension = Path.GetExtension(file.FileName);
                using MemoryStream? fileUploadStream = new();
                await file.CopyToAsync(fileUploadStream);
                fileUploadStream.Position = 0;
                var imageName = Guid.NewGuid().ToString() + imageExtension;
                var blob = _blobContainer.GetBlobClient(imageName);
                var result = await blob.UploadAsync(fileUploadStream);
                var blobResponse = result.GetRawResponse();
                if (blobResponse.Status == 201)
                    return new BlobResponseDto
                    {
                        IsSuccess = true,
                        Url = blob.Uri.AbsoluteUri
                    };

                return new BlobResponseDto
                {
                    IsSuccess = false,
                    Errors = $"Не удалось сохранить блоб. Код состояния: {blobResponse.Status}, Описание: {blobResponse.ReasonPhrase}"
                };
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "Ошибка при загрузки файла.");
                return new BlobResponseDto { IsSuccess = false };
            }
        }
    }
}
