using CatalogoGames.Domain.Entity;

namespace CatalogoGames.Domain.Repository
{
    public interface IGameRepository
    {
        Task<Game?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Game>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Game game, CancellationToken ct = default);
        Task UpdateAsync(Game game, CancellationToken ct = default);
        Task DeleteAsync(Game game, CancellationToken ct = default);
    }

}
