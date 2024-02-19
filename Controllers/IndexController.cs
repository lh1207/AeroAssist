using Microsoft.AspNetCore.Mvc;

namespace AeroAssist.Controllers
{
    /// <summary>
    /// Handles HTTP requests related to the index page.
    /// </summary>
    public class IndexController : Controller
    {
        /// <summary>
        /// Returns the Index view.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}