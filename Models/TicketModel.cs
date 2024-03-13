using System.Text;
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
            var response = await _client.GetAsync($"https://localhost:7223/api/Ticket/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Tickets = JsonConvert.DeserializeObject<IEnumerable<Ticket>>(json);
                return RedirectToPage("Success");
            }
            else
            {
                Tickets = new List<Ticket>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Ticket ticket)
        {
            var json = JsonConvert.SerializeObject(ticket);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7223/api/Ticket", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            // else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPutAsync(Ticket ticket)
        {
            var json = JsonConvert.SerializeObject(ticket);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("https://localhost:7223/api/Ticket", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            // else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnDeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"https://localhost:7223/api/Ticket/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            // else
            {
                return Page();
            }
        }
    }
}