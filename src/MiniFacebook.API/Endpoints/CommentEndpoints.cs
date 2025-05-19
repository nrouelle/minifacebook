using Microsoft.EntityFrameworkCore;
using MiniFacebook.Application.DTOs;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data;

namespace MiniFacebook.API.Endpoints
{
    public static class CommentEndpoints
    {
        public static void MapCommentEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/posts/{postId:guid}/comments");

            // Get comments for a post
            group.MapGet("/", async (Guid postId, AppDbContext db) =>
            {
                var comments = await db.Comments
                    .Where(c => c.PostId == postId)
                    .Include(c => c.Author)
                    .OrderBy(c => c.CreatedAt)
                    .ToListAsync();

                return Results.Ok(comments);
            });

            // Create a comment on a post
            group.MapPost("/", async (Guid postId, CreateCommentDto dto, AppDbContext db) =>
            {
                var comment = new Comment
                {
                    PostId = postId,
                    Text = dto.Text,
                    AuthorId = dto.AuthorId
                };

                db.Comments.Add(comment);
                await db.SaveChangesAsync();

                return Results.Created($"/api/comments/{comment.Id}", comment);
            });

            // Delete a comment
            app.MapDelete("/api/comments/{id:guid}", async (Guid id, AppDbContext db) =>
            {
                var comment = await db.Comments.FindAsync(id);
                if (comment is null) return Results.NotFound();

                db.Comments.Remove(comment);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }

}
