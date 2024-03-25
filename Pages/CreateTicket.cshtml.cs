using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AeroAssist.Pages;

public class CreateTicket : PageModel
{
    public class CreateTicketModel(HttpClient client, Ticket ticket) : PageModel
    {
        [BindProperty]
        public Ticket Ticket { get; set; } = ticket;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await client.PostAsJsonAsync("https://localhost:7223/api/Ticket/Ticket", Ticket);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Success");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the ticket.");
                return Page();
            }
        }
    }
}