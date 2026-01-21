using MediatR;

namespace CatalogoGames.Application.Games.Commands
{
    public record DeleteGameCommand(Guid Id) : IRequest<bool>;

}
