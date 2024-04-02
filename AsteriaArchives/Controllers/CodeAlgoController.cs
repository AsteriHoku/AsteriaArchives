using Microsoft.AspNetCore.Mvc;

namespace AsteriaArchives.Controllers;
// code &&|| algorithms
public class CodeAlgoController : Controller {
    public async Task<IActionResult> Index() {
        return View();
    }
    
    public async Task<IActionResult> Euler() {
        //todo just add favs in view & link to euler & link to any of my githubs assoc with euler & ch euler
        return View();
    }
}