using CatalogoGames.Domain.Entity;
using FluentAssertions;

namespace Catalogo.UnitTests.Domain
{
    public class GameTestsDomain
    {
        [Fact]
        public void CreateGame_ShouldSetPropertiesCorrectly()
        {
            var game = new Game("Halo", "Xbox", 199.99m, new DateTime(2021, 12, 8));

            game.Title.Should().Be("Halo");
            game.Platform.Should().Be("Xbox");
            game.Price.Should().Be(199.99m);
        }

        [Fact]
        public void UpdateGame_ShouldChangeProperties()
        {
            var game = new Game("Halo", "Xbox", 199.99m, DateTime.Now);
            game.Update("Halo Infinite", "Xbox Series", 299.99m, DateTime.Now, "Shooter", "Microsoft");

            game.Title.Should().Be("Halo Infinite");
            game.Platform.Should().Be("Xbox Series");
            game.Price.Should().Be(299.99m);
            game.Description.Should().Be("Shooter");
            game.Publisher.Should().Be("Microsoft");
        }

        [Fact]
        public void CreateGame_ShouldThrowException_WhenTitleIsEmpty()
        {
            Action act = () => new Game("", "Xbox", 199.99m, DateTime.Now);
            act.Should().Throw<ArgumentException>().WithMessage("Título obrigatório.");
        }
    }
}