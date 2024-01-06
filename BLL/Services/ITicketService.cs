using BLL.DTOs;
using BLL.UnitOfWork;
using DAL.Models;

namespace BLL.Services;

public interface ITicketService
{
    Task<Ticket?> AddTicketAsync(TicketDto ticketDto);
    Task<Order?> AddOrderAsync(OrderDto orderDto);
    Task<IEnumerable<Order>> GetOrdersAsync(bool? paid = null);
    Task<Order?> UpdateOrderAsync(int id);
}

public class TicketService : ITicketService
{
    public TicketService(IUnitOfWork workModel)
    {
        WorkModel = workModel;
    }

    private IUnitOfWork WorkModel { get; }

    public async Task<Ticket?> AddTicketAsync(TicketDto ticketDto)
    {
        var ticket = new Ticket
        {
            Name = ticketDto.Name,
            Price = ticketDto.Price,
            Amount = ticketDto.Amount,
            SpectacleId = ticketDto.SpectacleId
        };

        await WorkModel.Tickets.AddTicketAsync(ticket);
        await WorkModel.SaveChangesAsync();

        return ticket;
    }

    public async Task<Order?> AddOrderAsync(OrderDto orderDto)
    {
        var ticket = await WorkModel.Tickets
            .GetTicketAsync(orderDto.TicketId);

        if (ticket is null)
            return null;

        if (ticket.Amount < orderDto.Quantity)
            return null;

        ticket.Amount -= orderDto.Quantity;

        var order = new Order
        {
            TicketId = orderDto.TicketId,
            IsPaid = orderDto.IsPaid,
            Quantity = orderDto.Quantity
        };

        await WorkModel.Tickets.AddOrderAsync(order);
        await WorkModel.SaveChangesAsync();

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(bool? paid = null)
    {
        return await WorkModel.Tickets.GetOrdersAsync(paid);
    }

    public async Task<Order?> UpdateOrderAsync(int id)
    {
        var order = await WorkModel.Tickets.GetOrderAsync(id);

        if (order is null)
            return null;

        order.IsPaid = true;
        await WorkModel.SaveChangesAsync();

        return order;
    }
}
