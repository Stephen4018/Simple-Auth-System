using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple_Authentication_System_Application.Dtos;
using Simple_Authentication_System_Application.Interfaces;
using Simple_Authentication_System_Domain.Common;
using System.Security.Claims;

namespace Simple_Authentication_System_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 201)]
        public async Task<IActionResult> Register(RegisterUserDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterUserAsync(registerDto);
                return CreatedAtAction(nameof(GetCurrentUser), new { }, result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 200)]
        public async Task<IActionResult> Login(LoginUserDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetLoggedinUser")]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = CurrentUser.UserId;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid))
            {
                return Unauthorized();
            }

            try
            {
                var user = await _authService.GetUserByIdAsync(userGuid);
                return Ok(user);
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("validate")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public ActionResult ValidateToken()
        {
            return Ok(new { isValid = true });
        }
    }
}
