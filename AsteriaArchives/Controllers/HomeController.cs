using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AsteriaArchives.Models;

namespace AsteriaArchives.Controllers;

public class HomeController : Controller
{
    //todo
    //update client so it's factory and using scope or something
    //debug current state
    //play with custom range slider for frontend
    //create dropdown navbar
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Sources()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}