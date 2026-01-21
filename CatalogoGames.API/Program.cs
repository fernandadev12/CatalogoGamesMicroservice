using Azure.Messaging.ServiceBus;
using Catalogo.Infra.Messaging;
using Catalogo.Infra.Persistence;
using Catalogo.Infra.Repositories;
using CatalogoGames.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração de serviços
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configuração do pipeline HTTP
ConfigurePipeline(app);

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // EF Core + SQLite
    services.AddDbContext<CatalogoDbContext>(opt =>
        opt.UseSqlite(configuration.GetConnectionString("Sqlite")));

    // Repositórios e UoW
    services.AddScoped<IGameRepository, GameRepository>();
    services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CatalogoDbContext>());

    // MediatR (scan no assembly da Application)
    services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblyContaining<CatalogoGames.Application.Games.Commands.CreateGameCommand>());
    

    // Controllers + Swagger + HealthChecks
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddHealthChecks();
}

static void ConfigurePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.MapControllers();
    app.MapHealthChecks("/health");

    // Inicializa consumidor do Service Bus
    var consumer = app.Services.GetRequiredService<GameEventsConsumer>();
    _ = consumer.StartAsync();

    app.Lifetime.ApplicationStopping.Register(() =>
        consumer.StopAsync().GetAwaiter().GetResult());
}