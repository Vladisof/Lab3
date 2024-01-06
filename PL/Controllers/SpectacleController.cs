using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[Route("/")]
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

        return View(spectacles);
    }

    [HttpPost]
    public async Task<IActionResult> Index(string searchBy, string searchValue)
    {
        var spectacles = await SpectacleService
            .GetSpectaclesAsync(searchBy, searchValue);

        return View("Index", spectacles);
    }
}
