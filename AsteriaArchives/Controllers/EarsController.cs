using Microsoft.AspNetCore.Mvc;

namespace AsteriaArchives.Controllers;

public class EarsController : Controller {
    public async Task<IActionResult> Index() {
        return View();
    }
}