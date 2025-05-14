using Microsoft.AspNetCore.Mvc;
using Simple_Authentication_System_Domain.Common;
using System.Security.Claims;

namespace Simple_Authentication_System_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public LoggedInUser CurrentUser
        {
            get
            {
                return new LoggedInUser(User as ClaimsPrincipal);
            }
        }
    }
}
