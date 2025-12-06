
using System.Net;
using System.Net.Http.Json;
using CatalogoGames.Application.Games.Commands;
using CatalogoGames.Application.Games.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CatalogoGames.Test.IntegrationTest
{
    public class GamesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public GamesControllerTests(WebApplicationFactory<Program> factory)
        {
            // Cria client para a API
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostGame_ShouldCreateGame_AndReturnId()
        {
            var command = new CreateGameCommand(
                Title: "Halo Infinite",
                Platform: "Xbox",
                Price: 299.90m,
                ReleaseDate: new DateTime(2021, 12, 8),
                Description: "Shooter futurista",
                Publisher: "Microsoft"
            );

            var response = await _client.PostAsJsonAsync("/api/games", command);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var id = await response.Content.ReadFromJsonAsync<Guid>();
            id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetGames_ShouldReturnList()
        {
            var response = await _client.GetAsync("/api/games");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var games = await response.Content.ReadFromJsonAsync<List<GameViewModel>>();
            games.Should().NotBeNull();
        }

        [Fact]
        public async Task GetGameById_ShouldReturnNotFound_WhenGameDoesNotExist()
        {
            var response = await _client.GetAsync($"/api/games/{Guid.NewGuid()}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}