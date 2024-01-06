namespace DAL.Models;

public class Order
{
    public int Id { get; set; }

    public int Quantity { get; set; }
    public bool IsPaid { get; set; }

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;
}
