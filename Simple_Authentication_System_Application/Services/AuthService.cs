using Domain.Entities;
using Domain.Interfaces;
using Serilog;
using Simple_Authentication_System_Application.Dtos;
using Simple_Authentication_System_Application.Interfaces;
using Simple_Authentication_System_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Authentication_System_Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        //private readonly ILogger _logger;
        public AuthService(IUnitofWork unitofWork, IPasswordService passwordService, ITokenService tokenService)
        {
            _unitofWork = unitofWork;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<object>> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await _unitofWork.UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new ApplicationException("User not found");
                }

                var response = new UserDto();
                response.Id = user.Id;
                response.Username = user.Username;
                response.Email = user.Email;
                response.CreatedAt = user.CreatedAt;

                return new SuccessApiResponse<object>("User Fetched Successfully", response, 200);
            }
            catch (Exception)
            {
                return new ApiResponse<object>("error occurred while Fetching USer", null, 400);
            }
        }

        public async Task<ApiResponse<object>> LoginAsync(LoginUserDto loginDto)
        {
            try
            {
                var user = await _unitofWork.UserRepository.GetUserByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    throw new ApplicationException("Invalid Credentials");
                }

                bool isPasswordValid = _passwordService.VerifyPassword(loginDto.Password, user.PasswordHash);
                if(!isPasswordValid)
                {
                    throw new ApplicationException("Invalid Credentials");
                }

                user.LastLogin = DateTime.UtcNow;
                await _unitofWork.UserRepository.UpdateAsync(user);
                await _unitofWork.CompleteAsync();

                // Generate token
                string token = _tokenService.CreateToken(user);

                var AuthResponse = new AuthResponseDto();
                AuthResponse.Token = token;
                AuthResponse.User.Id = user.Id;
                AuthResponse.User.Username = user.Username;
                AuthResponse.User.Email = user.Email;
                AuthResponse.User.CreatedAt = user.CreatedAt;
                //AuthResponse.Expiration = DateTime.UtcNow.AddDays(7);
                return new SuccessApiResponse<object>("LoggedIn Successfully", AuthResponse, 200);
            }
            catch (Exception)
            {
                return new ApiResponse<object>("An error occurred while Logging in", null, 400);
            }
        }

        public async Task<ApiResponse<object>> RegisterUserAsync(RegisterUserDto registerDto)
        {
            try
            {
                var ifUserExist = await _unitofWork.UserRepository.UserExistsAsync(registerDto.Email, registerDto.Username);
                if (ifUserExist)
                {
                    throw new ApplicationException("User with this Email or Username Already Exist");
                }

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = registerDto.Email,
                    PasswordHash = _passwordService.HashPassword(registerDto.Password),
                    CreatedAt = DateTime.UtcNow,
                    Username = registerDto.Username,
                };

                

                await _unitofWork.UserRepository.AddAsync(user);
                await _unitofWork.CompleteAsync();


                string token = _tokenService.CreateToken(user);

                var AuthResponse = new AuthResponseDto();
                AuthResponse.User.Id = user.Id;
                AuthResponse.User.Username = user.Username;
                AuthResponse.User.Email = user.Email;
                AuthResponse.User.CreatedAt = user.CreatedAt;
                //AuthResponse.Expiration = DateTime.UtcNow.AddDays(7);
                //AuthResponse.Token = token;


                return new SuccessApiResponse<object>("User Created Successfully", AuthResponse, 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>($"An error occurred while Creating User {ex.Message}", null, 400);
            }
        }
    }
}
