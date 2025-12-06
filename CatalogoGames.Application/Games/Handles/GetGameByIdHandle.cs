
using Catalogo.Application.Games.Queries;
using CatalogoGames.Application.Games.ViewModels;
using CatalogoGames.Domain.Repository;
using MediatR;

namespace Catalogo.Application.Games.Handlers;

public class GetGameByIdHandler : IRequestHandler<GetGameByIdQuery, GameViewModel?>
{
    private readonly IGameRepository _repo;
    public GetGameByIdHandler(IGameRepository repo) => _repo = repo;

    public async Task<GameViewModel?> Handle(GetGameByIdQuery request, CancellationToken ct)
    {
        var g = await _repo.GetByIdAsync(request.Id, ct);
        return g is null ? null : new GameViewModel(g.Id, g.Title, g.Description, g.Platform, g.Publisher, g.Price, g.ReleaseDate);
    }
}

public class GetAllGamesHandler : IRequestHandler<GetAllGamesQuery, IReadOnlyList<GameViewModel>>
{
    private readonly IGameRepository _repo;
    public GetAllGamesHandler(IGameRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<GameViewModel>> Handle(GetAllGamesQuery request, CancellationToken ct)
    {
        var all = await _repo.GetAllAsync(ct);
        return all.Select(g => new GameViewModel(g.Id, g.Title, g.Description, g.Platform, g.Publisher, g.Price, g.ReleaseDate)).ToList();
    }
}