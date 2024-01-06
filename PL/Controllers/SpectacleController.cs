using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SpectacleController : Controller
{
    public SpectacleController(ISpectacleService spectacleService)
    {
        SpectacleService = spectacleService;
    }

    private ISpectacleService SpectacleService { get; }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var spectacles = await SpectacleService.GetSpectaclesAsync();

        return Json(spectacles);
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromBody] SearchDto dto)
    {
        var spectacles = await SpectacleService
            .GetSpectaclesAsync(dto.SearchBy, dto.SearchValue);

        return Json(spectacles);
    }
}
