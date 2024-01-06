using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/order/[action]")]
public class OrderController : Controller
{
    public OrderController(ITicketService ticketService)
    {
        TicketService = ticketService;
    }

    private ITicketService TicketService { get; }

    [HttpGet]
    [ActionName("")]
    public async Task<IActionResult> Index()
    {
        var orders = await TicketService.GetOrdersAsync();

        return Json(orders);
    }

    [HttpPost]
    [ActionName("make")]
    public async Task<IActionResult> MakeOrder([FromBody] OrderDto dto)
    {
        var res = await TicketService.AddOrderAsync(dto);

        if (res is null)
            return BadRequest("Вільних квитків немає");

        return Json("Замовлення успішно створено");
    }

    [HttpPost]
    [ActionName("update")]
    public async Task<IActionResult> UpdateOrder([FromBody] int id)
    {
        var res = await TicketService.UpdateOrderAsync(id);

        if (res is null)
            return BadRequest("Щось пішло не так");

        var orders = await TicketService.GetOrdersAsync();

        return Json(orders);
    }
}
