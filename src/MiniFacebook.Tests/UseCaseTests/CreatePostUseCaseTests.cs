using MiniFacebook.Application.DTOs.Posts;
using MiniFacebook.Application.UseCases.Posts;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;
using Moq;

namespace MiniFacebook.Tests.UseCaseTests
{
    public class CreatePostUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldCreatePost_WhenDataIsValid()
        {
            // Arrange
            var mockRepo = new Mock<IPostRepository>();
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            mockUserRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User("name", "email@mail.fr", ""));

            var useCase = new CreatePost(mockRepo.Object, mockUserRepo.Object);

            var authorEmail = "email@mail.fr";
            var content = "Hello world!";
            var dto = new CreatePostDto(content, authorEmail);

            Post? capturedPost = null;
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Callback<Post>(p => capturedPost = p)
                .Returns(Task.CompletedTask);

            // Act
            var result = await useCase.ExecuteAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(authorEmail, result.Author);
            Assert.Equal(content, result.Content);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Post>()), Times.Once);

            // Optionally check the exact object passed to repo
            Assert.NotNull(capturedPost);
            Assert.Equal(dto.Content, capturedPost!.Content);
            Assert.Equal(dto.AuthorEmail, capturedPost.Author.Email);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrow_WhenContentIsEmpty()
        {
            // Arrange
            var mockRepo = new Mock<IPostRepository>();
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            mockUserRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User("name", "email@mail.fr", ""));

            var useCase = new CreatePost(mockRepo.Object, mockUserRepo.Object);

            var dto = new CreatePostDto(string.Empty, string.Empty);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(dto));
            Assert.Contains("Content cannot be empty", exception.Message);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrow_WhenAuthorDoesNotExist()
        {
            // Arrange
            var mockPostRepo = new Mock<IPostRepository>();
            var mockUserRepo = new Mock<IUserRepository>();

            mockUserRepo.Setup(r => r.ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var useCase = new CreatePost(mockPostRepo.Object, mockUserRepo.Object);

            var dto = new CreatePostDto("Valid content", string.Empty);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(dto));
            Assert.Equal("Author not found", exception.Message);
        }

    }
}
