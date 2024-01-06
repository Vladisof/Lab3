using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[Route("/order/[action]")]
public class OrderController : Controller
{
    public OrderController(ITicketService ticketService)
    {
        TicketService = ticketService;
    }

    private ITicketService TicketService { get; }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var orders = await TicketService.GetOrdersAsync();

        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> MakeOrder(int ticketId, bool paid)
    {
        var dto = new OrderDto
        {
            TicketId = ticketId,
            IsPaid = paid,
            Quantity = 1
        };

        var res = await TicketService.AddOrderAsync(dto);

        if (res is null)
            return BadRequest("Вільних квитків немає");

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateOrder(int id)
    {
        var res = await TicketService.UpdateOrderAsync(id);

        if (res is null)
            return BadRequest("Щось пішло не так");

        return RedirectToAction("Index");
    }
}
