using Microsoft.AspNetCore.Mvc;

namespace AeroAssist.Controllers
{
    public class LoginController: Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
