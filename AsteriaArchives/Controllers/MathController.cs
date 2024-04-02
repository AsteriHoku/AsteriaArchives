using Microsoft.AspNetCore.Mvc;

namespace AsteriaArchives.Controllers;

public class MathController : Controller {
    //wolfram!
    //add link to euler in AA
    public async Task<IActionResult> Pi()
    {
        return View();
    }
}