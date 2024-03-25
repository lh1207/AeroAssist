using System.Text;
using AeroAssist.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AeroAssist.Models
{
    [BindProperties(SupportsGet = true)]
    public class TicketModel(AeroAssistContext context, ILogger<TicketModel> logger) : PageModel
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

        public async Task OnGet(int? id)
        {
            logger.LogInformation("OnGet method called");

            if (id.HasValue)
            {
                await OnGetById(id.Value);
            }
            else
            {
                await OnGetAll();
            }
        }


        // needed for OnGet
        public List<Ticket>? AllTickets { get; set; }

        public async Task OnGetAll()
        {
            logger.LogInformation("OnGetAll method called");
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
            }
        }

        public async Task<IActionResult> OnGetById(int id)
        {
            logger.LogInformation("OnGetById method called");
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Ticket/{id}");

            var client = GetHttpClientWithHandler();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Ticket = JsonConvert.DeserializeObject<Ticket>(responseStream);

                if (Ticket != null) return Page();
                logger.LogError($"Ticket with id {id} not found");
                return RedirectToPage("/Error");
            }
            else
            {
                logger.LogError($"Failed to fetch ticket: {response.StatusCode}");
                return RedirectToPage("/Error");
            }
            return Page();
        }

        // needed for OnPost
        public Ticket? Ticket { get; set; }

        // Post to create a new ticket via API endpoint then redirect to success page if success. If not, redirect
        // to error page and show error.
        // Form data is parsed through Request.Form to determine if the request is a PUT or DELETE request.
        public async Task<IActionResult> OnPost(Ticket ticket)
        {
            logger.LogInformation($"Form data before parsing id: {Request.Form}");

            if (Request.Form["_method"] == "put")
            {
                logger.LogInformation($"id from form data: {Request.Form["id"]}");

                if (int.TryParse(Request.Form["id"], out int id)) // Parse the id from the form data
                {
                    logger.LogInformation($"OnPut method called with ID: {id}");
                    ticket.TicketId = id; // Set the TicketId in the ticket object
                    return await OnPut(id, ticket);
                }
                else
                {
                    logger.LogError("Failed to parse id from form data");
                    return Page();
                }
            }

            if (Request.Form["_method"] == "delete")
            {
                if (int.TryParse(Request.Form["id"], out int id)) // Parse the id from the form data
                {
                    return await OnDelete(id, ticket);
                }
                else
                {
                    logger.LogError("Failed to parse id from form data");
                    return Page();
                }
            }

            logger.LogInformation($"Form data after parsing id: {Request.Form}");

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

        public async Task<IActionResult> OnPut(int id, Ticket updatedTicket)
        {
            var existingTicket = await context.Tickets.FindAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            existingTicket.Title = updatedTicket.Title;
            existingTicket.Description = updatedTicket.Description;
            existingTicket.Status = updatedTicket.Status;
            existingTicket.Priority = updatedTicket.Priority;
            existingTicket.Type = updatedTicket.Type;
            existingTicket.Assignee = updatedTicket.Assignee;
            existingTicket.Reporter = updatedTicket.Reporter;
            existingTicket.Created = updatedTicket.Created;
            existingTicket.Updated = updatedTicket.Updated;
            existingTicket.Due = updatedTicket.Due;
            existingTicket.Resolution = updatedTicket.Resolution;
            existingTicket.Comments = updatedTicket.Comments;
            existingTicket.Attachments = updatedTicket.Attachments;
            existingTicket.Departments = updatedTicket.Departments;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Page();
        }

        private bool TicketExists(int id)
        {
            return context.Tickets.Any(e => e.TicketId == id);
        }

        public async Task<IActionResult> OnDelete(int id, Ticket deletedTicket)
        {
            var existingTicket = await context.Tickets.FindAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            context.Tickets.Remove(existingTicket);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Ticket");
        }
    }
}