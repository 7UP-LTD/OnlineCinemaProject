namespace OnlineCinema.WebApi.ApiDescriptors
{
    /// <summary>
    /// Класс, представляющий информацию об API.
    /// </summary>
    public class ApiInfo
    {
        /// <summary>
        /// Получает или задает название API.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Получает или задает версию API.
        /// </summary>
        public string Version { get; set; } = null!;

        /// <summary>
        /// Получает или задает описание API.
        /// </summary>
        public string Description { get; set; } = null!; 
    }
}
