using Newtonsoft.Json;

namespace AeroAssist.Models
{
    public class TicketModel
    {
        private readonly IHttpClientFactory _clientFactory;
        public List<Ticket>? Tickets { get; set; }

        public TicketModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGetAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Ticket");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Tickets = JsonConvert.DeserializeObject<List<Ticket>>(responseStream);
            }
        }
    }
}