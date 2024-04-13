using AeroAssist.Data;
using Microsoft.AspNetCore.Mvc;

namespace AeroAssist.Controllers
{
    public class GraphController : Controller
    {
        private readonly AeroAssistContext _context;

        public GraphController(AeroAssistContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.GetAllTicketsAsync();

            // Process the data into a format suitable for your graphs
            // This is just an example, replace with your own logic
            var data = tickets.GroupBy(t => t.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() });

            // Pass the data to the view
            ViewData["GraphData"] = data;

            return View();
        }
    }
}