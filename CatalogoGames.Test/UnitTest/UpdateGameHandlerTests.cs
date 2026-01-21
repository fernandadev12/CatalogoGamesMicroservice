using Catalogo.Application.Games.Commands;
using Catalogo.Application.Games.Handlers;
using CatalogoGames.Domain.Entity;
using CatalogoGames.Domain.Repository;
using FluentAssertions;
using Moq;

namespace Catalogo.UnitTests.Application
{
    public class UpdateGameHandlerTests
    {
        private readonly Mock<IGameRepository> _repoMock = new();
        private readonly Mock<IUnitOfWork> _uowMock = new();

        [Fact]
        public async Task Handle_ShouldUpdateGame_WhenGameExists()
        {
            // Arrange
            var game = new Game("Halo", "Xbox", 199.99m, DateTime.Now);
            _repoMock.Setup(r => r.GetByIdAsync(game.Id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(game);

            var handler = new UpdateGameHandler(_repoMock.Object, _uowMock.Object);
            var command = new UpdateGameCommand(game.Id, "Halo Infinite", "Xbox Series", 299.99m, DateTime.Now, "Shooter", "Microsoft");

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            game.Title.Should().Be("Halo Infinite");
            _repoMock.Verify(r => r.UpdateAsync(game, It.IsAny<CancellationToken>()), Times.Once);
            _uowMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenGameDoesNotExist()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Game?)null);

            var handler = new UpdateGameHandler(_repoMock.Object, _uowMock.Object);
            var command = new UpdateGameCommand(Guid.NewGuid(), "Halo Infinite", "Xbox Series", 299.99m, DateTime.Now, "Shooter", "Microsoft");

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();
        }
    }
}