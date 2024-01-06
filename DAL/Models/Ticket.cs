namespace DAL.Models;

public class Ticket
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }

    public int SpectacleId { get; set; }
    public Spectacle Spectacle { get; set; } = null!;
}
