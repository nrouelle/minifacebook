using MiniFacebook.Application.DTOs;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Posts
{
    public class CreatePost
    {
        private readonly IPostRepository _postRepository;

        public CreatePost(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        /// <summary>
        /// Creates a new post and saves it to the repository.
        /// </summary>
        /// <param name="dto">The data required to create a post.</param>
        /// <returns>The created post.</returns>
        public async Task<Post> ExecuteAsync(CreatePostDto dto)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                AuthorId = dto.AuthorId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _postRepository.AddAsync(post);
            return post;
        }
    }
}
