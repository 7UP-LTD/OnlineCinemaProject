using Microsoft.OpenApi.Models;

namespace OnlineCinema.WebApi.ApiDescriptors
{
    /// <summary>
    /// Класс, предоставляющий информацию о API.
    /// </summary>
    public class ApiDescriptor
    {
        /// <summary>
        /// Получает информацию о API из конфигурации.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <returns>Объект OpenApiInfo, содержащий информацию о API.</returns>
        public static OpenApiInfo GetApiInfo(IConfiguration configuration)
        {
            var companyInfo = configuration.GetSection("Company").Get<CompanyInfo>()!;
            var licenseInfo = configuration.GetSection("License").Get<LicenseInfo>()!;
            var apiInfo = configuration.GetSection("ApiInfo").Get<ApiInfo>()!;
            return new()
            {
                Title = apiInfo.Title,
                Version = apiInfo.Version,
                Description = apiInfo.Description,
                Contact = new OpenApiContact
                {
                    Name = companyInfo.Name,
                    Email = companyInfo.Email,
                    Url = new Uri(companyInfo.SiteUrl)
                },
                License = new OpenApiLicense
                {
                    Name = licenseInfo.Name,
                    Url = new Uri(licenseInfo.Url)
                }
            };
        } 
    }
}
