namespace PlayerManagementService.Models;

public record Player
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int TotalWins { get; set; } = 0;
}
