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



    public record JogoVm(Guid Id, string Titulo, string Descricao, decimal Preco, string Plataforma, DateTime Lancamento, bool Ativo);
    public record CriarJogoVm(string Titulo, string Descricao, decimal Preco, string Plataforma, DateTime Lancamento);
    public record AtualizarJogoVm(string Titulo, string Descricao, decimal Preco, string Plataforma, DateTime Lancamento);
    public record IniciarCompraVm(Guid JogoId, int Quantidade);

}