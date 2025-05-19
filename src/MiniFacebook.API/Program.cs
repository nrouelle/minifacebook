using MiniFacebook.Application.DTOs;
using MiniFacebook.Application.Services;
using MiniFacebook.Infrastructure.Data;
using MiniFacebook.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Domain.Interfaces;
using MiniFacebook.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("MiniFacebookDB"));
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

app.MapAuthEndpoints();
app.MapPostEndpoints();

app.Run();
