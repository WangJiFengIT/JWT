using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;
using WebApi.Services;
using WebApi.Util;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private DbContext _dbContext;

        public AccountController(JwtHelper jwtHelper, DbContext dbContext)
        {
            _jwtHelper = jwtHelper;
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Login([FromBody] UserInfo userInfo)
        {
            return Ok(new { token = _jwtHelper.CreateToken(userInfo) });
        }

        [HttpGet]
        public ActionResult GetUser()
        {
            SysUser user = new SysUser()
            {
                UserName = "韦德",
                Account = "Wade",
                Password = Guid.NewGuid().ToString(),
                Phone = "13928460767",
                CreateTime = DateTime.Now
            };
            _dbContext.ReadWrite().Add(user);
            _dbContext.SaveChanges();


            var users = _dbContext.Read().Set<SysUser>().ToList();
            Thread.Sleep(5000);
            var users2 = _dbContext.Read().Set<SysUser>().ToList();
            return Ok(new { data1 = users, data2 = users2 });
        }
    }
}
