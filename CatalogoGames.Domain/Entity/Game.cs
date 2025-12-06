namespace CatalogoGames.Domain.Entity
{
    public class Game
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = default!;
        public string? Description { get; private set; }
        public string Platform { get; private set; } = default!;
        public string? Publisher { get; private set; }
        public decimal Price { get; private set; }
        public DateTime ReleaseDate { get; private set; }

        private Game() { } // EF
        public Game(string title, string platform, decimal price, DateTime releaseDate, string? description = null, string? publisher = null)
        {
            Id = Guid.NewGuid();
            Update(title, platform, price, releaseDate, description, publisher);
        }

        public void Update(string title, string platform, decimal price, DateTime releaseDate, string? description, string? publisher)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Título obrigatório.");
            if (string.IsNullOrWhiteSpace(platform)) throw new ArgumentException("Plataforma obrigatória.");
            if (price < 0) throw new ArgumentException("Preço inválido.");

            Title = title.Trim();
            Platform = platform.Trim();
            Price = price;
            ReleaseDate = releaseDate;
            Description = description?.Trim();
            Publisher = publisher?.Trim();
        }
    }
}
