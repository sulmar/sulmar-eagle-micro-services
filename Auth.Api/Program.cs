using Auth.Api.Models;
using Auth.Api.Validators;
using Auth.Domain;
using Auth.Infrastructure;
using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserIdentityRepository, InMemoryUserIdentityRepository>();
builder.Services.AddScoped<IEnumerable<UserIdentity>>(f => new UserIdentityFaker(new PasswordHasher<UserIdentity>()).Generate(10));
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();



var app = builder.Build();


// POST api/token/create

app.MapPost("api/token/create", (
    IValidator<LoginRequest> validator,
    LoginRequest request, 
    IAuthService authService,
    ITokenService tokenService,
    HttpResponse response
    ) =>
{
    var validationResult = validator.Validate(request);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    if (authService.TryAuthorize(request.Login, request.Password, out UserIdentity userIdentity))
    {
        var token = tokenService.CreateToken(userIdentity);

        response.Cookies.Append("your-cookie", token);

        return Results.Ok(token);
    }

    return Results.BadRequest(new { message = "Username or password is incorrect." });

});

app.MapGet("/", () => "Hello World!");

app.Run();
