using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.WebApi.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;


        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost("list")]
        public IActionResult GetMovies(int page, int pageSize)
        {
            var moviesList = _movieService.GetMovies(page, pageSize, null);
            return Ok(moviesList);
        }
    }
}