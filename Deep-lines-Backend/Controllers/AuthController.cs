using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
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
        private readonly IConfiguration configuration;
        public AuthController(IAuthService authService , IConfiguration configuration)
        {
            this.configuration = configuration;
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
        [AllowAnonymous]
        public IActionResult Refresh( RefreshTokenPostDTO refreshRequest)
        {
            if (refreshRequest == null) return BadRequest();

            var response = authService.RefreshToken(refreshRequest.UserId, refreshRequest.Token);
            if (response == null) return Unauthorized();

            return Ok(response);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (changePasswordDTO == null) return BadRequest();
            var checkFailer = await authService.ChangePassword(changePasswordDTO);
            if (checkFailer != null)
            {
                return BadRequest(new
                {
                    code = checkFailer.code,
                    message = checkFailer.message
                });
            }
            return Ok();
        }
        [Authorize (Roles = "Administration")]
        [HttpPost("reset-password/{id}")]
        public async Task<IActionResult> ResetPasswordToDefult(int id)
        {
            var checkFailer = await authService.resetPasswordToDefult(id);
            if (checkFailer != null)
            {
                return BadRequest(new
                {
                    code = checkFailer.code,
                    message = checkFailer.message
                });
            }
            return Ok(new
            {
                code = 200,
                newPassword = configuration["Employee:DefaultPassword"],
            });
        }
        [HttpPost("delete-refreshToken")]
        public async Task<IActionResult> deleteRefreshToken(string token)
        {
            authService.deleteRefreshToken(token);
            return Ok();
        }
    }
}
