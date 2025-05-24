using MiniFacebook.Application.DTOs.Posts;
using MiniFacebook.Application.Interfaces;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Posts
{
    public class CreatePost: ICreatePost
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
        public async Task<PostDto> ExecuteAsync(CreatePostDto dto)
        {
            var author = await _userRepository.GetByEmailAsync(dto.AuthorEmail);
            if (author == null)
                throw new InvalidOperationException("Author not found");

            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentException("Content cannot be empty");

            var post = new Post(author, dto.Content);

            await _postRepository.AddAsync(post);

            var postCreated = new PostDto
            (post.Id.Value,
                post.Content,
                new AuthorDto(post.Author.FullName, post.Author.Email),
                post.CreatedAt);
            return postCreated;
        }
    }
}
