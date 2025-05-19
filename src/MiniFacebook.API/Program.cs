using MiniFacebook.Application.DTOs;
using MiniFacebook.Application.Services;
using MiniFacebook.Infrastructure.Data;
using MiniFacebook.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("MiniFacebookDB"));


// DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

app.MapPost("/auth/register", async (RegisterRequest request, AuthService authService) =>
{
    try
    {
        await authService.RegisterAsync(request);
        return Results.Ok("User registered successfully.");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.Run();
