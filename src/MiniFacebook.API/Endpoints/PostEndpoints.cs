using Microsoft.EntityFrameworkCore;
using MiniFacebook.Application.DTOs;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data;

namespace MiniFacebook.API.Endpoints
{
    public static class PostEndpoints
    {
        public static void MapPostEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/posts");

            group.MapGet("/", async (AppDbContext db) =>
            {
                var posts = await db.Posts
                    .Include(p => p.Author)
                    .Include(p => p.Comments)
                    .Include(p => p.Likes)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();

                return Results.Ok(posts);
            });

            group.MapPost("/", async (CreatePostDto dto, AppDbContext db) =>
            {
                var post = new Post
                {
                    Content = dto.Content,
                    ImageUrl = dto.ImageUrl,
                    AuthorId = dto.AuthorId
                };

                db.Posts.Add(post);
                await db.SaveChangesAsync();

                return Results.Created($"/api/posts/{post.Id}", post);
            });

            group.MapDelete("/{id:guid}", async (Guid id, AppDbContext db) =>
            {
                var post = await db.Posts.FindAsync(id);
                if (post == null) return Results.NotFound();
                db.Posts.Remove(post);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }

}
