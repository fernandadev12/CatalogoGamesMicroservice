using MediatR;

namespace CatalogoGames.Application.Games.Commands;

public record CreateGameCommand(
    string Title,
    string Platform,
    decimal Price,
    DateTime ReleaseDate,
    string? Description,
    string? Publisher
) : IRequest<Guid>;
