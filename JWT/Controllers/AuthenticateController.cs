using JWT.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{

    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginInput input)
        {
            //从数据库验证用户名，密码 
            //验证通过 否则 返回Unauthorized

            //创建claim
            var authClaims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub,input.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            //标识PII是否显示在日志中。错误的默认。
            //算法HS256要求SecurityKey.KeySize大于128位，并且您的密钥只有48位.通过添加至少10个符号来扩展它. 至于"PII隐藏"部分，
            //这是GDPR合规性工作的一部分，以隐藏日志中的任何堆栈或变量信息.您应通过以下方式启用其他详细信息:
            IdentityModelEventSource.ShowPII = true;
            //签名密钥
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecureKeySecureKeySecureKeySecureKeySecureKeySecureKey"));
            //生成token
            var token = new JwtSecurityToken(
                   issuer: "https://www.cnblogs.com/chengtian",
                   audience: "https://www.cnblogs.com/chengtian",
                   expires: DateTime.Now.AddHours(2),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                   );
            //返回token和过期时间
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}
