using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Authentication_System_Domain.Common
{
    public class LoggedInUser : ClaimsPrincipal
    {
        public LoggedInUser(ClaimsPrincipal principal) : base(principal)
        {

        }

        public string? UserId
        {
            get
            {
                if (!(Identity is ClaimsIdentity identity))
                    return null;
                var claims = Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                return claims!.Value;
            }
        }
    }
}
