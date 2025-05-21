using MiniFacebook.Application.UseCases.Posts;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;
using Moq;

namespace MiniFacebook.Tests.UseCaseTests
{
    public class GetAllPostsUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnAllPosts()
        {
            // Arrange
            var mockRepo = new Mock<IPostRepository>();

            var posts = new List<Post>
            {
                new Post(Guid.NewGuid(), Guid.NewGuid(), new("First post"), DateTime.UtcNow),
                new Post(Guid.NewGuid(), Guid.NewGuid(), new("Second post"), DateTime.UtcNow)
            };

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(posts);

            var useCase = new GetAllPosts(mockRepo.Object);

            // Act
            var result = (await useCase.ExecuteAsync()).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("First post", result[0].Content);
            Assert.Equal("Second post", result[1].Content);

            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoPostsExist()
        {
            // Arrange
            var mockRepo = new Mock<IPostRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Post>());

            var useCase = new GetAllPosts(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrow_WhenRepositoryThrows()
        {
            // Arrange
            var mockRepo = new Mock<IPostRepository>();

            mockRepo.Setup(r => r.GetAllAsync())
                    .ThrowsAsync(new Exception("Database failure"));

            var useCase = new GetAllPosts(mockRepo.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync());
            Assert.Equal("Database failure", exception.Message);

            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

    }
}
