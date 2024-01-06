namespace BLL.DTOs;

public class OrderDto
{
    public int Quantity { get; set; }
    public bool IsPaid { get; set; }
    public int TicketId { get; set; }
}
