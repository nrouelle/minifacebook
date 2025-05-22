using MiniFacebook.Application.DTOs.Posts;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Posts
{
    public class GetPost
    {
        private readonly IPostRepository _postRepository;

        public GetPost(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        /// <summary>
        /// Retrieves a post by its ID.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>The post if found, or null.</returns>
        public async Task<PostDto?> ExecuteAsync(Guid postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                return null;

            return new PostDto
            {
                Id = post.Id.Value,
                Author = post.Author.FullName,
                Content = post.Content,
                CreatedAt = post.CreatedAt
            };
        }
    }
}
