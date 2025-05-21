using MiniFacebook.Application.DTOs;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Posts
{

    public class GetAllPosts
    {
        private readonly IPostRepository _postRepository;

        public GetAllPosts(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        /// <summary>
        /// Retrieves all posts, ordered by creation date descending.
        /// </summary>
        public async Task<IEnumerable<PostDto>> ExecuteAsync()
        {
            var posts = await _postRepository.GetAllAsync();

            return posts.Select(p => new PostDto
            {
                Id = p.Id,
                AuthorId = p.AuthorId,
                Content = p.Content,
                CreatedAt = p.CreatedAt
            });
        }
    }
}
