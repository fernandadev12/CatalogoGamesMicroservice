
using CatalogoGames.Application.Games.ViewModels;
using MediatR;

namespace Catalogo.Application.Games.Queries;

public record GetAllGamesQuery() : IRequest<IReadOnlyList<GameViewModel>>;
