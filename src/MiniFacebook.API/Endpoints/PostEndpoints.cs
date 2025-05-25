using MiniFacebook.Application.DTOs.Posts;
using MiniFacebook.Application.Interfaces.Posts;
using MiniFacebook.Application.Interfaces.Users;

namespace MiniFacebook.API.Endpoints
{
    public static class PostEndpoints
    {
        public static void MapPostEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/posts");

            group.RequireAuthorization();

            group.MapGet("/", async (IGetAllPosts getAllPosts) =>
            {
                var posts = await getAllPosts.ExecuteAsync();

                return Results.Ok(posts);
            });

            group.MapPost("/", async (CreatePostDto dto, ICreatePost createPost, ICheckUserExists checkUserExists) =>
            {
                var authorExists = await checkUserExists.ExecuteAsync(dto.AuthorEmail);
                if(!authorExists) 
                    return Results.NotFound("User not found");
                
                var postCreated = await createPost.ExecuteAsync(dto);

                return Results.Created($"/api/posts/{postCreated.Id}", postCreated);
            });
        }
    }

}
