
using Catalogo.Application.Games.Commands;
using CatalogoGames.Domain.Repository;
using MediatR;

namespace Catalogo.Application.Games.Handlers;

public class UpdateGameHandler : IRequestHandler<UpdateGameCommand, bool>
{
    private readonly IGameRepository _repo;
    private readonly IUnitOfWork _uow;

    public UpdateGameHandler(IGameRepository repo, IUnitOfWork uow)
    {
        _repo = repo; _uow = uow;
    }

    public async Task<bool> Handle(UpdateGameCommand request, CancellationToken ct)
    {
        var game = await _repo.GetByIdAsync(request.Id, ct);
        if (game is null) return false;
        game.Update(request.Title, request.Platform, request.Price, request.ReleaseDate, request.Description, request.Publisher);
        await _repo.UpdateAsync(game, ct);
        await _uow.SaveChangesAsync(ct);
        return true;
    }
}