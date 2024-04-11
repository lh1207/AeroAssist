using AeroAssist.Data;

namespace AeroAssist.Services
{
/// <summary>
/// Provides services for managing tickets in the AeroAssist system.
/// </summary>
    public class TicketService(AeroAssistContext context, ILogger<TicketService> logger) : TicketService.ITicketService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketService"/> class.
        /// </summary>
        public interface ITicketService
        {
            IEnumerable<Ticket?> GetAllTickets();
            Ticket? GetTicketById(int id);
            Ticket? CreateTicket(Ticket? ticket);
            void UpdateTicket(Ticket? updatedTicket);
            void DeleteTicket(int id);
        }

        /// <summary>
        /// Retrieves all tickets.
        /// </summary>
        /// <returns>A list of all tickets.</returns>
        public IEnumerable<Ticket?> GetAllTickets()
        {
            logger.LogInformation("GetAllTickets method called");
            return context.Tickets.ToList();
        }

        /// <summary>
        /// Retrieves a ticket by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket.</param>
        /// <returns>The ticket with the specified ID, or null if no such ticket exists.</returns>
        public Ticket? GetTicketById(int id)
        {
            var ticket = context.Tickets.Find(id);
            if (ticket == null)
            {
                logger.LogError($"No ticket found with ID: {id}");
            }
            else
            {
                logger.LogInformation($"Fetched ticket with ID: {id}");
            }
            return ticket;
        }

        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="ticket">The ticket to create.</param>
        /// <returns>The created ticket.</returns>
        public Ticket? CreateTicket(Ticket? ticket)
        {
            if (ticket == null)
            {
                logger.LogError("CreateTicket method called with null ticket");
                throw new ArgumentNullException(nameof(ticket));
            }

            logger.LogInformation($"Creating ticket with title: {ticket.Title}");
            context.Tickets.Add(ticket);
            context.SaveChanges();

            return ticket;
        }

        /// <summary>
        /// Updates a ticket.
        /// </summary>
        /// <param name="updatedTicket">The updated ticket.</param>
        public void UpdateTicket(Ticket? updatedTicket)
        {
            if (updatedTicket == null)
            {
                throw new ArgumentNullException(nameof(updatedTicket));
            }

            context.Tickets.Update(updatedTicket);
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes a ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to delete.</param>
        public void DeleteTicket(int id)
        {
            var ticket = context.Tickets.Find(id);
            if (ticket != null)
            {
                context.Tickets.Remove(ticket);
                context.SaveChanges();
            }
        }
    }
}