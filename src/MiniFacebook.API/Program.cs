using MiniFacebook.Application.DTOs;
using MiniFacebook.Application.Services;
using MiniFacebook.Infrastructure.Data;
using MiniFacebook.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Domain.Interfaces;
using MiniFacebook.API.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MiniFacebook.API;
using Microsoft.OpenApi.Models;
using MiniFacebook.Application;

var builder = WebApplication.CreateBuilder(args);

// EF Core
//builder.Services.AddDbContext<AppDbContext>(opt =>
//    opt.UseInMemoryDatabase("MiniFacebookDB"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// Swagger documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mini Facebook API",
        Version = "v1"
    });

    // Authentification JWT
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Entrez un token JWT valide (format : Bearer {token})",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition("Bearer", securityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme, Array.Empty<string>()
        }
    });
});


// DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mini Facebook API v1");
});


app.MapAuthEndpoints();
app.MapPostEndpoints();
app.MapCommentEndpoints();
app.MapLikeEndpoints();
app.MapUserEndpoints();

app.Run();
