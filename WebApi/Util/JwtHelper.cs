using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Model;

namespace WebApi.Util
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public string CreateToken(UserInfo userInfo)
        {
            // 1. 定义需要使用到的Claims
            var claims = new[]
            {
                //iss(Issuser)：代表这个JWT的签发主体；
                //sub(Subject)：代表这个JWT的主体，即它的所有人；
                //aud(Audience)：代表这个JWT的接收对象；
                //exp(Expiration time)：是一个时间戳，代表这个JWT的过期时间；
                //nbf(Not Before)：是一个时间戳，代表这个JWT生效的开始时间，意味着在这个时间之前验证JWT是会失败的；
                //iat(Issued at)：是一个时间戳，代表这个JWT的签发时间；
                //jti(JWT ID)：是JWT的唯一标识。
                //主题
                new Claim(JwtRegisteredClaimNames.Sub,userInfo.UserName),
                //jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            // 2. 从 appsettings.json 中读取SecretKey
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            // 3. 选择加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;
            // 4. 生成Credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);
            // 5. 根据以上，生成token
            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],     //Issuer
                _configuration["Jwt:Audience"],   //Audience
                claims,                          //Claims,
                DateTime.Now,                    //notBefore
                DateTime.Now.AddSeconds(30),    //expires
                signingCredentials               //Credentials
            );
            // 6. 将token变为string
            // 过期时间：jwtSecurityToken.ValidTo
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
