using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AeroAssist.Pages
{
    public class TicketModel : PageModel
    {
        private readonly ILogger<TicketModel> _logger;
        private readonly IHttpClientFactory _clientFactory;
        public List<Ticket>? Tickets { get; set; }

        public TicketModel(IHttpClientFactory clientFactory, ILogger<TicketModel> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task OnGet()
        {
            _logger.LogInformation("OnGetAsync method called");
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
                _logger.LogError($"Failed to fetch tickets: {response.StatusCode}");
                Tickets = new List<Ticket>(); // Initialize Tickets as an empty list
            }
        }
    }
}