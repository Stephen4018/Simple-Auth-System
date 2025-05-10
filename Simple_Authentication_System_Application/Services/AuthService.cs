using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Authentication_System_Application.Services
{
    public class AuthService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        public AuthService(IUnitofWork unitofWork, IPasswordService passwordService, ITokenService tokenService)
        {
            _unitofWork = unitofWork;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }


    }
}
