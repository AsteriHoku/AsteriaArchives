using Microsoft.AspNetCore.Mvc;

namespace AsteriaArchives.Controllers;

public class HackathonController : Controller
{
    [Route("/Hackathon")]
    public IActionResult Index()
    {
        return View();
    }
    
}