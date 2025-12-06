
using CatalogoGames.Application.Games.Commands;
using CatalogoGames.Domain.Repository;
using MediatR;

namespace Catalogo.Application.Games.Handlers;

public class DeleteGameHandler : IRequestHandler<DeleteGameCommand, bool>
{
    private readonly IGameRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteGameHandler(IGameRepository repo, IUnitOfWork uow)
    {
        _repo = repo; _uow = uow;
    }

    public async Task<bool> Handle(DeleteGameCommand request, CancellationToken ct)
    {
        var game = await _repo.GetByIdAsync(request.Id, ct);
        if (game is null) return false;
        await _repo.DeleteAsync(game, ct);
        await _uow.SaveChangesAsync(ct);
        return true;
    }
}