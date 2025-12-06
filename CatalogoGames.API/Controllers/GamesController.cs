using Microsoft.AspNetCore.Mvc;
using MediatR;
using Catalogo.Application.Games.Commands;
using Catalogo.Application.Games.Queries;
using CatalogoGames.Application.Games.ViewModels;
using CatalogoGames.Application.Games.Commands;

namespace Catalogo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GameViewModel>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllGamesQuery(), ct);
            return Ok(result);
        }

        // GET: api/games/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GameViewModel>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetGameByIdQuery(id), ct);
            if (result is null) return NotFound();
            return Ok(result);
        }

        // POST: api/games
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateGameCommand command, CancellationToken ct)
        {
            var id = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // PUT: api/games/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGameCommand command, CancellationToken ct)
        {
            var ok = await _mediator.Send(command with { Id = id }, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        // DELETE: api/games/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteGameCommand(id), ct);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}