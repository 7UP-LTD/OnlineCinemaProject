
using Microsoft.EntityFrameworkCore;

namespace OnlineCinema.Data
{
    /// <summary>
    /// Класс контекста базы данных
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Конструктор класса ApplicationDbContext, который принимает настройки контекста базы данных.
        /// </summary>
        /// <param name="options">Настройки контекста базы данных.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
