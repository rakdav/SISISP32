using Lab3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection = builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login",async(User loginData,ModelDB db) =>
{
    User? person = await db.Users!.FirstOrDefaultAsync(p => p.EMail == loginData.EMail &&
p.Password == loginData.Password);
    if (person is null) return Results.Unauthorized();
    var claims = new List<Claim> { new Claim(ClaimTypes.Email, person.EMail!) };
    var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.Now.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = person.EMail
    };
    return Results.Json(response);
});

app.MapGet("/api/groups", [Authorize] async (ModelDB db) => await db.Groups!.ToListAsync());
app.MapGet("api/group/{id:int}", [Authorize] async (ModelDB db, int id) => await db.Groups!.Where(g => g.Id == id).FirstOrDefaultAsync());
app.MapGet("/api/students", [Authorize] async (ModelDB db) => await db.Students!.ToListAsync());
app.MapGet("/api/group/{name}", [Authorize] async (ModelDB db,string name) => await db.Groups!.Where(u=>u.Name==name).ToListAsync());
app.MapPost("/api/group", [Authorize] async (Group group, ModelDB db) =>
{
    await db.Groups!.AddAsync(group);
    await db.SaveChangesAsync();
    return group;
});
app.MapPost("/api/student", [Authorize] async (Student student, ModelDB db) =>
{
    await db.Students!.AddAsync(student);
    await db.SaveChangesAsync();
    return student;
});
app.MapDelete("/api/group/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Group? group = await db.Groups!.FirstOrDefaultAsync(u => u.Id == id);
    if (group == null) return Results.NotFound(new { message = "Группа не найдена" });
    db.Groups!.Remove(group);
    await db.SaveChangesAsync();
    return Results.Json(group);
});
app.MapDelete("/api/student/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Student? student = await db.Students!.FirstOrDefaultAsync(u => u.Id == id);
    if (student == null) return Results.NotFound(new { message = "Студент не найден" });
    db.Students!.Remove(student);
    await db.SaveChangesAsync();
    return Results.Json(student);
});
app.MapPut("/api/group", [Authorize] async (Group group, ModelDB db) =>
{
    Group? g = await db.Groups!.FirstOrDefaultAsync(u => u.Id == group.Id);
    if (g == null) return Results.NotFound(new { message = "Группа не найдена" });
    g.Name = group.Name;
    g.Speciality = group.Speciality;
    g.Faculty = group.Faculty;
    await db.SaveChangesAsync();
    return Results.Json(g);
});
app.MapPut("/api/student", [Authorize] async (Student student, ModelDB db) =>
{
    Student? st = await db.Students!.FirstOrDefaultAsync(u => u.Id == student.Id);
    if (st == null) return Results.NotFound(new { message = "Группа не найдена" });
    st.Name = student.Name;
    st.FirstName = student.FirstName;   
    st.LastName = student.LastName;
    st.GroupId = student.GroupId;
    await db.SaveChangesAsync();
    return Results.Json(st);
});
app.Run();
