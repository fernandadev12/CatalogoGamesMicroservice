using CatalogoGames.Application.Games.ViewModels;
using MediatR;

namespace Catalogo.Application.Games.Queries;

public record GetGameByIdQuery(Guid Id) : IRequest<GameViewModel?>;
