using Auth.Api.Models;
using Auth.Domain;
using Auth.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserIdentityRepository, InMemoryUserIdentityRepository>();
builder.Services.AddScoped<IEnumerable<UserIdentity>>(f => new UserIdentityFaker(new PasswordHasher<UserIdentity>()).Generate(10));
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();


// POST api/token/create

app.MapPost("api/token/create", (
    AuthUserModel model, 
    IAuthService authService,
    ITokenService tokenService,
    HttpResponse response
    ) =>
{    
    if (authService.TryAuthorize(model.Login, model.Password, out UserIdentity userIdentity))
    {
        var token = tokenService.CreateToken(userIdentity);

        response.Cookies.Append("your-cookie", token);

        return Results.Ok(token);
    }

    return Results.BadRequest(new { message = "Username or password is incorrect." });

});

app.MapGet("/", () => "Hello World!");

app.Run();
