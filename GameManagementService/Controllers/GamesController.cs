using Microsoft.AspNetCore.Mvc;
using GameManagementService.Models;

namespace GameManagementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly HttpClient _playerServiceClient;
    private static readonly Random _random = new();

    public GamesController(IHttpClientFactory httpClientFactory)
    {
        _playerServiceClient = httpClientFactory.CreateClient("PlayerServiceClient");
    }

    private static readonly List<Gametype> _gametypes = new()
    {
        new() { Id = 1, Name = "Poker" },
        new() { Id = 2, Name = "Blackjack" },
        new() { Id = 3, Name = "Roulette" }
    };

    [HttpGet]
    public IActionResult GetGametypes()
    {
        return Ok(_gametypes);
    }

    [HttpPost("{id}/play")]
    public async Task<IActionResult> PlayGame(int id, IEnumerable<int> playerIds)
    {
        var selectedGametype = _gametypes.FirstOrDefault(g => g.Id == id);

        if (selectedGametype is null)
            return NotFound($"Gametype with ID {id} not found.");

        var selectedPlayers = new List<PlayerInfo>();

        foreach (var playerId in playerIds)
        {
            var response = await _playerServiceClient.GetAsync($"/api/players/{playerId}");
            if (!response.IsSuccessStatusCode)
                continue;

            var playerInfo = await response.Content.ReadFromJsonAsync<PlayerInfo>();
            if (playerInfo is null)
                continue;

            selectedPlayers.Add(playerInfo);
        }

        var chosenWinner = selectedPlayers[_random.Next(0, selectedPlayers.Count)];

        var updatedTotalWins = chosenWinner.TotalWins + 1;
        var updateSuccess = await UpdateWinnerTotalWins(chosenWinner.Id, updatedTotalWins);

        if (updateSuccess)
            chosenWinner.TotalWins = updatedTotalWins;  // Update locally as well

        return Ok
        (
            $"""
            Game {selectedGametype.Name} played.
            Participants: {string.Join(", ", selectedPlayers.Select(p => p.Name))}.
            Winner: {chosenWinner.Name}, with a total wins of {chosenWinner.TotalWins}.
            """
        );
    }

    private async Task<bool> UpdateWinnerTotalWins(int playerId, int newTotalWins)
    {
        var playerUpdate = new { TotalWins = newTotalWins };
        var response = await _playerServiceClient.PutAsJsonAsync($"/api/players/{playerId}/totalwins", playerUpdate);

        return response.IsSuccessStatusCode;
    }
}
