using Lesson8;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var people=new List<Person>()
{
    new Person{Email="kosha@mail.ru",Password="123456"},
    new Person{Email="zinovievmaksim@mail.ru",Password="11111"}
};
var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer=AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience=AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey=AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey=true
    };
});
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login", (Person loginData) =>
{
    Person? person = people.FirstOrDefault(p => p.Email == loginData.Email &&
p.Password == loginData.Password);
    if (person is null) return Results.Unauthorized();
    var claims = new List<Claim> { new Claim(ClaimTypes.Email, person.Email!) };
    var jwt = new JwtSecurityToken(issuer:AuthOptions.ISSUER,
        audience:AuthOptions.AUDIENCE,
        claims:claims,
        expires:DateTime.Now.Add(TimeSpan.FromMinutes(2)),
        signingCredentials:new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256)
        );
    var encoderJWT= new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = person.Email
    };
    return Results.Json(response);
});
app.Map("/hello", [Authorize] () =>new { message="Hello world!" });
app.Map("/", () => "Home Page");
app.Run();
