using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AeroAssist.Pages
{
    public class TicketModel(ILogger<TicketModel> logger)
        : PageModel
    {
        public List<Ticket>? Tickets { get; set; }

        public async Task OnGet()
        {
            logger.LogInformation("OnGetAsync method called");
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Ticket/Ticket");

            var handler = new HttpClientHandler();

            // TODO: Remove this line before production
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var client = new HttpClient(handler);

            client.BaseAddress = new Uri("https://localhost:7223/");
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
    }
}