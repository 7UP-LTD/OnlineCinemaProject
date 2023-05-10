namespace OnlineCinema.WebApi.ApiDescriptors
{
    /// <summary>
    /// Класс, для хранения информации о компании из файла JSON.
    /// </summary>
    public class CompanyInfo
    {
        /// <summary>
        /// Получает или задает название компании.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Получает или задает электронную почту компании.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Получает или задает URL сайта компании.
        /// </summary>
        public string SiteUrl { get; set; } = null!;
    }
}
