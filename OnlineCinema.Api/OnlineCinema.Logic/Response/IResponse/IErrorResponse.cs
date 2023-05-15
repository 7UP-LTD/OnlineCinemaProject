using OnlineCinema.Logic.Dtos;

namespace OnlineCinema.Logic.Response.IResponse
{
    public interface IErrorResponse
    {
        ErrorResponseDto InternalServerError();
    }
}
