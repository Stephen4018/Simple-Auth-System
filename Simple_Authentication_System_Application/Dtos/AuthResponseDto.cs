using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Authentication_System_Application.Dtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; } = new UserDto();
        public DateTime Expiration { get; set; }
    }
}
