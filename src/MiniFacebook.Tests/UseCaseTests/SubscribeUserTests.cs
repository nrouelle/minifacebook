using MiniFacebook.Application.DTOs.Subscriptions;
using MiniFacebook.Application.UseCases.Users;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;
using Moq;

namespace MiniFacebook.Tests.UseCaseTests
{
    public class SubscribeUserTests
    {
        [Fact]
        public async Task Should_Subscribe_When_SubscriptionDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<ISubscribeRepository>();
            var dto = new SubscriptionDto
            {
                SubscriberEmail = "alice@example.com",
                SubscribedToEmail = "bob@example.com"
            };

            mockRepo.Setup(r => r.SubscriptionExists(dto.SubscriberEmail, dto.SubscribedToEmail))
                    .Returns(false);

            var useCase = new SubscribeUser(mockRepo.Object);

            // Act
            await useCase.ExecuteAsync(dto);

            // Assert
            mockRepo.Verify(r => r.SubscribeAsync(It.Is<Subscription>(s =>
                s.SubscriberEmail == dto.SubscriberEmail &&
                s.SubscribedToEmail == dto.SubscribedToEmail
            )), Times.Once);
        }

        [Fact]
        public async Task Should_NotSubscribe_When_SubscriptionAlreadyExists()
        {
            // Arrange
            var mockRepo = new Mock<ISubscribeRepository>();
            var dto = new SubscriptionDto
            {
                SubscriberEmail = "alice@example.com",
                SubscribedToEmail = "bob@example.com"
            };

            mockRepo.Setup(r => r.SubscriptionExists(dto.SubscriberEmail, dto.SubscribedToEmail))
                    .Returns(true);

            var useCase = new SubscribeUser(mockRepo.Object);

            // Act
            await useCase.ExecuteAsync(dto);

            // Assert
            mockRepo.Verify(r => r.SubscribeAsync(It.IsAny<Subscription>()), Times.Never);
        }

        [Fact]
        public async Task Should_Throw_When_SubscribingToSelf()
        {
            // Arrange
            var mockRepo = new Mock<ISubscribeRepository>();
            var dto = new SubscriptionDto
            {
                SubscriberEmail = "user@example.com",
                SubscribedToEmail = "user@example.com"
            };

            var useCase = new SubscribeUser(mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(dto));

            mockRepo.Verify(r => r.SubscribeAsync(It.IsAny<Subscription>()), Times.Never);
        }

        [Fact]
        public async Task Should_Throw_When_EmailsAreEmpty()
        {
            // Arrange
            var mockRepo = new Mock<ISubscribeRepository>();
            var dto = new SubscriptionDto
            {
                SubscriberEmail = "",
                SubscribedToEmail = ""
            };

            var useCase = new SubscribeUser(mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(dto));
            mockRepo.Verify(r => r.SubscribeAsync(It.IsAny<Subscription>()), Times.Never);
        }
    }
}

