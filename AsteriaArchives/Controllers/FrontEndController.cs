using Microsoft.AspNetCore.Mvc;

namespace AsteriaArchives.Controllers;

public class FrontEndController : Controller
{
    [Route("/Wizardry")]
    public IActionResult Index()
    {
        return View();
    }
    
    [Route("/Wizardry/SeaGreen")]
    public IActionResult GradientSeaGreen()
    {
        return View();
    }
}