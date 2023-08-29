using Microsoft.AspNetCore.Mvc;
using PlayerManagementService.Models;

namespace PlayerManagementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private static readonly List<Player> _players = new()
    {
        new() { Id = ++_nextPlayerId, Name = "Khalidou" },
        new() { Id = ++_nextPlayerId, Name = "Thomason" },
        new() { Id = ++_nextPlayerId, Name = "Massiolo" }
    };
    private static int _nextPlayerId = 0;

    [HttpGet]
    public IActionResult GetPlayers()
    {
        return Ok(_players);
    }

    [HttpGet("{id}")]
    public IActionResult GetPlayer(int id)
    {
        var player = _players.FirstOrDefault(p => p.Id == id);

        if (player is not null)
            return Ok(player);
        return NotFound($"Player with ID {id} not found.");
    }

    [HttpPost("add")]
    public IActionResult AddPlayer(Player player)
    {
        _nextPlayerId = _players.Any() ? _players.Last().Id + 1 : _nextPlayerId + 1;
        if (!_players.Any())
            _nextPlayerId++;

        player.Id = _nextPlayerId;
        _players.Add(player);

        return Ok($"Player {player.Name} added.");
    }

    [HttpPut("{id}/totalwins")]
    public IActionResult IncreasePlayerWins(int id)
    {
        var match = _players.FirstOrDefault(p => p.Id == id);

        if (match is not null)
        {
            match.TotalWins++;
            return Ok($"Player {match.Name}'s total wins increased to {match.TotalWins}");
        }
        return NotFound($"Player with ID {id} not found.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePlayer(int id)
    {
        var match = _players.FirstOrDefault(p => p.Id == id);

        if (match is not null)
        {
            _players.Remove(match);
            return Ok($"Player {match.Name} removed.");
        }
        return NotFound($"Player with ID {id} not found.");
    }
}
