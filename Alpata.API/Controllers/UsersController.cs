using Alpata.Models;
using Alpata.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Alpata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel model)
        {
            try
            {
                var user = await _userService.Register(model);
                return Ok(user);
            }
            catch (Exception ex)
            {
                // InnerException detayını yakalayarak döndürelim
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest($"An error occurred while saving the entity changes. Details: {errorMessage}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var user = await _userService.Login(model);
                return Ok(new { message = "Login successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
