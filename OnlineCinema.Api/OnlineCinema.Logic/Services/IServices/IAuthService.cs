using OnlineCinema.Logic.Dtos.AuthDtos;

namespace OnlineCinema.Logic.Services.IServices
{
    public interface IAuthService 
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto model);

        Task<UserManagerResponse> LoginUserAsync(LoginUserDto model);
    }
}
