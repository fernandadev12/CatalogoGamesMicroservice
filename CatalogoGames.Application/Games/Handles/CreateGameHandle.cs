using CatalogoGames.Application.Games.Commands;
using CatalogoGames.Domain.Entity;
using CatalogoGames.Domain.Repository;
using MediatR;

namespace Catalogo.Application.Games.Handlers;

public class CreateGameHandler : IRequestHandler<CreateGameCommand, Guid>
{
    private readonly IGameRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateGameHandler(IGameRepository repo, IUnitOfWork uow)
    {
        _repo = repo; _uow = uow;
    }

    public async Task<Guid> Handle(CreateGameCommand request, CancellationToken ct)
    {
        var game = new Game(request.Title, request.Platform, request.Price, request.ReleaseDate, request.Description, request.Publisher);
        await _repo.AddAsync(game, ct);
        await _uow.SaveChangesAsync(ct);
        return game.Id;
    }
}