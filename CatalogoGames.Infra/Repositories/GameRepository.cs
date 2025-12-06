using Catalogo.Infra.Persistence;
using CatalogoGames.Domain.Entity;
using CatalogoGames.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infra.Repositories;

public class GameRepository : IGameRepository
{
    private readonly CatalogoDbContext _ctx;
    public GameRepository(CatalogoDbContext ctx) => _ctx = ctx;

    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _ctx.Games.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id, ct);

    public async Task<IReadOnlyList<Game>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Games.AsNoTracking().OrderBy(g => g.Title).ToListAsync(ct);

    public async Task AddAsync(Game game, CancellationToken ct = default) =>
        await _ctx.Games.AddAsync(game, ct);

    public Task UpdateAsync(Game game, CancellationToken ct = default)
    {
        _ctx.Games.Update(game);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Game game, CancellationToken ct = default)
    {
        _ctx.Games.Remove(game);
        return Task.CompletedTask;
    }
}