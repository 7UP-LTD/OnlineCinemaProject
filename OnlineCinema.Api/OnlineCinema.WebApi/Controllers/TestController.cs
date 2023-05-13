using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos;
using System.Security.Claims;

namespace OnlineCinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = user.Value;
            return Ok(new List<TestUser>
            {
                new TestUser {FirstName = "John", LastName = "Smith"},
                new TestUser {FirstName = "Amely", LastName = "Blant"}
            });
        }

        [HttpGet("UserConfirmEmail")]
        [AllowAnonymous]
        public IActionResult UserConfirmEmail(string userId, string token)
        {
            var userTest = new TestUser { FirstName = userId, LastName = token };
            return Ok(userTest);
        }
    }
}
