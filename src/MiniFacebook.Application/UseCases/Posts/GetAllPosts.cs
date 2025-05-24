using MiniFacebook.Application.DTOs.Posts;
using MiniFacebook.Application.Interfaces;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Posts
{

    public class GetAllPosts: IGetAllPosts
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

            return posts.Select(p => new PostDto(
                p.Id.Value,
                p.Content,
                new AuthorDto(p.Author.FullName, p.Author.Email),
                p.CreatedAt)).ToList();
        }
    }
}
