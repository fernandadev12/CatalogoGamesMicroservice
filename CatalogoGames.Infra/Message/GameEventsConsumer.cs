
using Azure.Messaging.ServiceBus;
using MediatR;
using System.Text.Json;
using Catalogo.Application.Games.Commands;
using CatalogoGames.Application.Games.Commands;

namespace Catalogo.Infra.Messaging;

public class GameEventsConsumer : IAsyncDisposable
{
    private readonly ServiceBusProcessor _processor;
    private readonly IMediator _mediator;

    public GameEventsConsumer(ServiceBusClient client, IMediator mediator, string queueName)
    {
        _mediator = mediator;
        _processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = 2,
            AutoCompleteMessages = false
        });

        _processor.ProcessMessageAsync += HandleMessageAsync;
        _processor.ProcessErrorAsync += args => Task.CompletedTask;
    }

    public async Task StartAsync() => await _processor.StartProcessingAsync();
    public async Task StopAsync() => await _processor.StopProcessingAsync();

    private async Task HandleMessageAsync(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        var envelope = JsonSerializer.Deserialize<GameEventEnvelope>(body);
        if (envelope is not null)
        {
            switch (envelope.Type)
            {
                case "GameCreated":
                    await _mediator.Send(new CreateGameCommand(
                        envelope.Payload.Title,
                        envelope.Payload.Platform,
                        envelope.Payload.Price,
                        envelope.Payload.ReleaseDate,
                        envelope.Payload.Description,
                        envelope.Payload.Publisher));
                    break;
                case "GameUpdated":
                    await _mediator.Send(new UpdateGameCommand(
                        envelope.Payload.Id,
                        envelope.Payload.Title,
                        envelope.Payload.Platform,
                        envelope.Payload.Price,
                        envelope.Payload.ReleaseDate,
                        envelope.Payload.Description,
                        envelope.Payload.Publisher));
                    break;
                case "GameDeleted":
                    await _mediator.Send(new DeleteGameCommand(envelope.Payload.Id));
                    break;
            }
        }

        await args.CompleteMessageAsync(args.Message);
    }

    public async ValueTask DisposeAsync() => await _processor.DisposeAsync();
}

public record GameEventEnvelope(string Type, GameEventPayload Payload);
public record GameEventPayload(
    Guid Id,
    string Title,
    string Platform,
    decimal Price,
    DateTime ReleaseDate,
    string? Description,
    string? Publisher
);