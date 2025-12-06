namespace CatalogoGames.Application.Games.ViewModels
{
    public record GameViewModel(
     Guid Id,
     string Title,
     string? Description,
     string Platform,
     string? Publisher,
     decimal Price,
     DateTime ReleaseDate
 );

}
