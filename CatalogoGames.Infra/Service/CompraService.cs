using CatalogoGames.Domain.Service;
using Microsoft.Extensions.Logging;

namespace CatalogoGames.Infra.Service
{
    public class CompraService : ICompraService
    {
        private readonly ILogger<CompraService> _logger;
        public CompraService(ILogger<CompraService> logger) => _logger = logger;

        public Task IniciarFluxoCompraAsync(Guid jogoId, int quantidade, CancellationToken ct = default)
        {
            _logger.LogInformation("Fluxo de compra iniciado: Jogo {JogoId}, Quantidade {Quantidade}", jogoId, quantidade);
            return Task.CompletedTask;
        }
    }

}
