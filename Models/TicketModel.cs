using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AeroAssist.Models
{
    [BindProperties(SupportsGet=true)]
    public class TicketModel(ILogger<TicketModel> logger) : PageModel
    {
        private HttpClient GetHttpClientWithHandler()
        {
            var handler = new HttpClientHandler();

            // TODO: Remove this line before production
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7223/");

            return client;
        }
        // needed for OnGet
        public List<Ticket>? Tickets { get; set; }
        public Ticket Ticket { get; set; }

        public async Task OnGet()
        {
            logger.LogInformation("OnGet method called");
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Ticket/Ticket");

            var client = GetHttpClientWithHandler();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Tickets = JsonConvert.DeserializeObject<List<Ticket>>(responseStream);
            }
            else
            {
                logger.LogError($"Failed to fetch tickets: {response.StatusCode}");
                Tickets = new List<Ticket>(); // Initialize Tickets as an empty list
            }
        }

        // Post to create a new ticket via API endpoint then redirect to success page if success. If not, redirect
        // to error page and show error.
        public async Task<IActionResult> OnPost(Ticket ticket)
        {
            logger.LogInformation("OnPost method called");

            var client = GetHttpClientWithHandler();
            var content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Ticket/", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Success");
            }
            else
            {
                logger.LogError($"Failed to create ticket: {response.ReasonPhrase}");
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPutAsync(int id, Ticket ticket)
        {
            var client = GetHttpClientWithHandler();
            var content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/Ticket/Ticket/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Success");
            }
            else
            {
                logger.LogError($"Failed to update ticket: {response.StatusCode}");
                return Page();
            }
        }

        public async Task<IActionResult> OnDeleteAsync(int id)
        {
            var client = GetHttpClientWithHandler();
            var response = await client.DeleteAsync($"api/Ticket/Ticket/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Success");
            }
            else
            {
                logger.LogError($"Failed to delete ticket: {response.StatusCode}");
                return Page();
            }
        }
    }
}
