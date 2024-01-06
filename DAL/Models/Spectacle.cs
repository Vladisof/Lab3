namespace DAL.Models;

public class Spectacle
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Genre { get; set; } = default!;
    public DateTime Date { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
