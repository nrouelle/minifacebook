using Microsoft.EntityFrameworkCore;
using MiniFacebook.Application.UseCases.Posts;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data;
using MiniFacebook.Infrastructure.Repositories;

namespace MiniFacebook.Tests.IntegrationTests
{
    public class GetAllPostsIntegrationTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnInsertedPosts_FromDatabase()
        {
            // Arrange : base InMemory
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Posts")
                .Options;

            using var context = new AppDbContext(options);
            var post1 = new Post(Guid.NewGuid(), Guid.NewGuid(), "Nadege", "Post 1", DateTime.UtcNow);
            var post2 = new Post(Guid.NewGuid(), Guid.NewGuid(), "Batiste", "Post 2", DateTime.UtcNow);

            var repo = new PostRepository(context);
            await repo.AddAsync(post1);
            await repo.AddAsync(post2);
            var useCase = new GetAllPosts(repo);

            // Act
            var result = (await useCase.ExecuteAsync()).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Content == "Post 1");
            Assert.Contains(result, p => p.Content == "Post 2");
        }
    }
}
