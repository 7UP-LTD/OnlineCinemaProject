using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineCinema.Logic.Dtos.BlobDtos;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс сервиса для работы с хранилищем данных Asuze Blob.
    /// </summary>
    public interface IBlobService 
    {
        /// <summary>
        /// Загружает файл в хранилище данных Azure Blob.
        /// </summary>
        /// <param name="image">Объект IFormFile, представляющий загружаемый файл.</param>
        /// <returns>Объект <see cref="BlobResponseDto"/>, содержащий информацию о результате загрузки.</returns>
        Task<BlobResponseDto> UploadFileAsync(IFormFile file);

        /// <summary>
        /// Удаляем файл из хранилища данных Azure Blob.
        /// </summary>
        /// <param name="imageName">URL файла в контейнере хранилища Azure Blob.</param>
        /// <returns>Объект <see cref="BlobResponseDto"/>, содержащий информацию о результате удаления.</returns>
        Task<BlobResponseDto> DeleteFileAsync(string fileUrl);
    }
}