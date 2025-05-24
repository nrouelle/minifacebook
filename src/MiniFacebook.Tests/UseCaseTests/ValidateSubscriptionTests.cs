using MiniFacebook.Application.UseCases.Subscriptions;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;
using Moq;

namespace MiniFacebook.Tests.UseCaseTests
{

    public class ValidateSubscriptionTests
    {
        private readonly Mock<ISubscriptionRepository> _mockRepo;
        private readonly ValidateSubscription _useCase;

        public ValidateSubscriptionTests()
        {
            _mockRepo = new Mock<ISubscriptionRepository>();
            _useCase = new ValidateSubscription(_mockRepo.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ValidSubscription_ShouldValidateAndUpdate()
        {
            // Arrange
            var subscriberEmail = "john@example.com";
            var subscribedToEmail = "alice@example.com";
            var subscription = new Subscription(subscriberEmail, subscribedToEmail);

            _mockRepo.Setup(r => r.GetSubscriptionAsync(subscriberEmail, subscribedToEmail))
                .ReturnsAsync(subscription);

            _mockRepo.Setup(r => r.UpdateSubscriptionAsync(It.IsAny<Subscription>()))
                .Returns(Task.CompletedTask);

            // Act
            await _useCase.ExecuteAsync(subscriberEmail, subscribedToEmail);

            // Assert
            Assert.True(subscription.IsValidated);
            _mockRepo.Verify(r => r.UpdateSubscriptionAsync(subscription), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_SubscriptionDoesNotExist_ShouldThrow()
        {
            // Arrange
            var subscriberEmail = "john@example.com";
            var subscribedToEmail = "alice@example.com";

            _mockRepo.Setup(r => r.GetSubscriptionAsync(subscriberEmail, subscribedToEmail))
                .ReturnsAsync((Subscription?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _useCase.ExecuteAsync(subscriberEmail, subscribedToEmail));

            Assert.Equal("Subscription does not exist.", ex.Message);
            _mockRepo.Verify(r => r.UpdateSubscriptionAsync(It.IsAny<Subscription>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_AlreadyValidated_ShouldThrow()
        {
            // Arrange
            var subscriberEmail = "john@example.com";
            var subscribedToEmail = "alice@example.com";
            var subscription = new Subscription(subscriberEmail, subscribedToEmail);
            subscription.Validate(); // already validated

            _mockRepo.Setup(r => r.GetSubscriptionAsync(subscriberEmail, subscribedToEmail))
                .ReturnsAsync(subscription);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _useCase.ExecuteAsync(subscriberEmail, subscribedToEmail));

            Assert.Equal("Subscription is already validated.", ex.Message);
            _mockRepo.Verify(r => r.UpdateSubscriptionAsync(It.IsAny<Subscription>()), Times.Never);
        }
    }
}