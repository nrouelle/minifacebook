using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Application.DTOs;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data;

namespace MiniFacebook.API.Endpoints
{
    public static class LikeEndpoints
    {
        public static void MapLikeEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/posts/{postId:guid}/like");

            group.RequireAuthorization();

            // Like a post
            group.MapPost("/", async (Guid postId, LikeDto dto, AppDbContext db) =>
            {
                var exists = await db.Likes.AnyAsync(l => l.PostId == postId && l.UserId == dto.UserId);
                if (exists) return Results.BadRequest("Already liked");

                var like = new Like
                {
                    PostId = postId,
                    UserId = dto.UserId
                };

                db.Likes.Add(like);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            // Unlike a post
            group.MapDelete("/", async (Guid postId, [FromBody] LikeDto dto, AppDbContext db) =>
            {
                var like = await db.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == dto.UserId);
                if (like is null) return Results.NotFound();

                db.Likes.Remove(like);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }

}
