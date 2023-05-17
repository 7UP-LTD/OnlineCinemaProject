using System;
using Microsoft.AspNetCore.Identity;

namespace OnlineCinema.Data.Entities
{
    /// <summary>
    /// Представляет сущность роли в приложении.
    /// Наследование от IdentityRole с переопределением ID на Guid.
    /// </summary>
    public class RoleEntity : IdentityRole<Guid>
    {
    }
}
