using MiniFacebook.Application.DTOs.Posts;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Posts
{
    public class CreatePost
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public CreatePost(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Creates a new post and saves it to the repository.
        /// </summary>
        /// <param name="dto">The data required to create a post.</param>
        /// <returns>The created post.</returns>
        public async Task<Post> ExecuteAsync(CreatePostDto dto)
        {
            var userExists = await _userRepository.ExistsAsync(dto.AuthorId);
            if (!userExists)
                throw new InvalidOperationException("Author not found");

            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentException("Content cannot be empty");

            var post = new Post();
            post.Create(dto.AuthorId, dto.Content);

            await _postRepository.AddAsync(post);
            return post;
        }
    }
}
