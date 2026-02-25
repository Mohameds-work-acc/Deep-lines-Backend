using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deep_lines_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            if (login == null) return BadRequest();

            var result = authService.Authenticate(login);
            if (result == null) return Unauthorized();

            return Ok(result);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenPostDTO refreshRequest)
        {
            if (refreshRequest == null) return BadRequest();

            var response = authService.RefreshToken(refreshRequest.UserId, refreshRequest.Token);
            if (response == null) return Unauthorized();

            return Ok(response);
        }
    }
}
