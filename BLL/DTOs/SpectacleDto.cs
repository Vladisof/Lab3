namespace BLL.DTOs;

public class SpectacleDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Genre { get; set; } = default!;
    public DateTime Date { get; set; }
}
