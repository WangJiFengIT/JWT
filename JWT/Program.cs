using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//添加认证服务
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,//是否验证Issuer
        ValidateAudience = true,//是否验证Audience
        ValidateLifetime = true,//是否验证失效时间
        ValidateIssuerSigningKey = true,//是否验证SecurityKey
        ValidAudience = "https://www.cnblogs.com/chengtian",//Audience
        ValidIssuer = "https://www.cnblogs.com/chengtian",//Issuer，这两项和前面签发jwt的设置一致  表示谁签发的Token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecureKeySecureKeySecureKeySecureKeySecureKeySecureKey"))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //身份认证
    app.UseAuthentication();
    //身份授权
    app.UseAuthorization();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
