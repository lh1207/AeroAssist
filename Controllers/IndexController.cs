using AeroAssist.Data;
using Microsoft.AspNetCore.Mvc;

namespace AeroAssist.Controllers
{
    /// <summary>
    /// Handles HTTP requests related to the index page.
    /// </summary>
    public class IndexController(AeroAssistContext context) : Controller
    {
        public IActionResult Index()
        {
            var tickets = context.Tickets.ToList();
            return View(tickets);
        }
    }
}