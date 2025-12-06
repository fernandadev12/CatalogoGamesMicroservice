
using MediatR;

namespace Catalogo.Application.Games.Commands;

public record UpdateGameCommand(
    Guid Id,
    string Title,
    string Platform,
    decimal Price,
    DateTime ReleaseDate,
    string? Description,
    string? Publisher
) : IRequest<bool>;