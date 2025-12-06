using Catalogo.Application.Games.Handlers;
using CatalogoGames.Application.Games.Commands;
using CatalogoGames.Domain.Entity;
using CatalogoGames.Domain.Repository;
using FluentAssertions;
using Moq;

namespace Catalogo.UnitTests.Application
{
    public class CreateGameHandlerTests
    {
        private readonly Mock<IGameRepository> _repoMock = new();
        private readonly Mock<IUnitOfWork> _uowMock = new();

        [Fact]
        public async Task Handle_ShouldCreateGame_AndReturnId()
        {
            // Arrange
            var handler = new CreateGameHandler(_repoMock.Object, _uowMock.Object);
            var command = new CreateGameCommand("Halo", "Xbox", 199.99m, DateTime.Now, "Shooter", "Microsoft");

            _uowMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(1);

            // Act
            var id = await handler.Handle(command, CancellationToken.None);

            // Assert
            id.Should().NotBeEmpty();
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
            _uowMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}