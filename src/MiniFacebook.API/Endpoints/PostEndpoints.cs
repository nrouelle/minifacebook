using MiniFacebook.Application.DTOs.Posts;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.API.Endpoints
{
    public static class PostEndpoints
    {
        public static void MapPostEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/posts");

            group.RequireAuthorization();

            group.MapGet("/", async (IPostRepository postRepository) =>
            {
                var posts = await postRepository.GetAllAsync();

                return Results.Ok(posts);
            });

            group.MapPost("/", async (CreatePostDto dto, IPostRepository postRepository, IUserRepository userRepository) =>
            {
                var author = await userRepository.GetByEmailAsync(dto.AuthorEmail);
                if(author == null) 
                    return Results.NotFound("User not found");
                
                var post = new Post(author, dto.Content);
                
                await postRepository.AddAsync(post);

                return Results.Created($"/api/posts/{post.Id}", post);
            });

            group.MapDelete("/{id:guid}", async (Guid id, IPostRepository postRepository) =>
            {
                var post = await postRepository.GetByIdAsync(id);
                if (post == null) return Results.NotFound();
                await postRepository.DeleteAsync(post);
                return Results.NoContent();
            });
        }
    }

}
