using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public interface ITicketRepository
{
    Task<Ticket> AddTicketAsync(Ticket ticket);
    Task<Order> AddOrderAsync(Order order);
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<Ticket?> GetTicketAsync(int id);
    Task<Order?> GetOrderAsync(int id);
    Task<IEnumerable<Order>> GetOrdersAsync(bool? paid = null);
}

public class TicketRepository : ITicketRepository
{
    private readonly TheaterDbContext _context;

    public TicketRepository(TheaterDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket> AddTicketAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        return ticket;
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        return order;
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        var res = await _context.Tickets
            .ToListAsync();

        return res;
    }

    public Task<Ticket?> GetTicketAsync(int id)
    {
        return _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(bool? paid = null)
    {
        if (paid == null)
            return await _context.Orders
                .Include(o => o.Ticket)
                .ThenInclude(t => t.Spectacle)
                .ToListAsync();

        return await _context.Orders
            .Where(o => o.IsPaid == paid)
            .Include(o => o.Ticket)
            .ThenInclude(t => t.Spectacle)
            .ToListAsync();
    }
}
