using MiniFacebook.Application.DTOs;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Comments
{
    public class AddComment
    {
        private readonly ICommentRepository _commentRepository;

        public AddComment(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> ExecuteAsync(AddCommentDto dto)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                PostId = dto.PostId,
                AuthorId = dto.UserId,
                Text = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);
            return comment;
        }
    }
}
