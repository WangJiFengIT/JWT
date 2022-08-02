using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Util;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        public AccountController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        [HttpPost]
        public ActionResult Login([FromBody] UserInfo userInfo)
        {
            return Ok(new { token = _jwtHelper.CreateToken(userInfo) });
        }
    }
}
