using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AeroAssist.Models
{
    public class TicketModel : PageModel
    {
        private readonly HttpClient _client;

        public IEnumerable<Ticket>? Tickets { get; set; }

        public TicketModel(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _client.GetAsync($"https://localhost:7223/swagger/api/Ticket/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Tickets = JsonConvert.DeserializeObject<IEnumerable<Ticket>>(json);
            }
            else
            {
                Tickets = new List<Ticket>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync(Ticket ticket)
        {
            var response = await _client.PostAsJsonAsync("https://localhost:7223/swagger/api/Ticket", ticket);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(Ticket ticket)
        {
            var response = await _client.PutAsJsonAsync($"https://localhost:7223/swagger/api/Ticket/{ticket.TicketId}", ticket);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"https://localhost:7223/swagger/api/Ticket/{id}");
            return RedirectToPage();
        }
    }
}