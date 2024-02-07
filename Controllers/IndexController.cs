using Microsoft.AspNetCore.Mvc;

namespace AeroAssist.Controllers;

public class IndexController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}