using Simple_Authentication_System_Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Authentication_System_Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginUserDto loginDto);
        Task<UserDto> GetUserByIdAsync(Guid userId);
    }
}
