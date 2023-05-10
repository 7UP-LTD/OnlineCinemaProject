namespace OnlineCinema.WebApi.ApiDescriptors
{
    /// <summary>
    /// Класс, представляющий информацию о лицензии из файла appsettings.json.
    /// </summary>
    public class LicenseInfo
    {
        /// <summary>
        /// Получает или задает название лицензии.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Получает или задает URL лицензии.
        /// </summary>
        public string Url { get; set; } = null!;
    }
}
