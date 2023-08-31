namespace MyBlazorApp.Data;

public record PlayerInfo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int TotalWins { get; set; }
}
