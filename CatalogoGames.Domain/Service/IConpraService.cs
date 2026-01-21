namespace CatalogoGames.Domain.Service
{
    public interface ICompraService
    {
        Task IniciarFluxoCompraAsync(Guid jogoId, int quantidade, CancellationToken ct = default);
    }

}
