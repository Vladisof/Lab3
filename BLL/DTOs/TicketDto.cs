namespace BLL.DTOs;

public class TicketDto
{
    public string? Name { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }

    public int SpectacleId { get; set; }
}
