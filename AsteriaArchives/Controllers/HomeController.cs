using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AsteriaArchives.Models;

namespace AsteriaArchives.Controllers;

public class HomeController : Controller
{
    //todo
    //check gitignore what files are in source control
    //design custom color palette and apply to site, add classes like sitecolor-background  
    //debug current state (fix all dev jokes)
    //revisit pun & misc jokes (they seem to have gotten a little too NSFW)
    //set up logger and add logs
    //play with custom range slider for frontend
    //search solution for todos
    //update bootstrap and fontawesome and debug after, be sure to check docs for breaking changes

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