using Simple_Authentication_System_Application.Dtos;
using Simple_Authentication_System_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Authentication_System_Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> RegisterUserAsync(RegisterUserDto registerDto);
        Task<ApiResponse<object>> LoginAsync(LoginUserDto loginDto);
        Task<ApiResponse<object>> GetUserByIdAsync(Guid userId);
    }
}
