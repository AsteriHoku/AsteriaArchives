using Microsoft.AspNetCore.Mvc;

namespace AsteriaArchives.Controllers;

public class RootsRainbowsController(ILogger<RootsRainbowsController> logger) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}