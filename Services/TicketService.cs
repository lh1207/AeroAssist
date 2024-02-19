namespace AeroAssist.Services
{
    /// <summary>
    /// Defines the operations for ticket management.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Retrieves all tickets.
        /// </summary>
        /// <returns>A collection of all tickets.</returns>
        IEnumerable<Ticket?> GetAllTickets();

        /// <summary>
        /// Retrieves a specific ticket by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket.</param>
        /// <returns>The ticket if found; otherwise, null.</returns>
        Ticket? GetTicketById(int id);

        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="ticket">The ticket to create.</param>
        /// <returns>The created ticket with assigned ID.</returns>
        /// <exception cref="ArgumentNullException">Thrown when ticket is null.</exception>
        Ticket? CreateTicket(Ticket? ticket);

        /// <summary>
        /// Updates a specific ticket.
        /// </summary>
        /// <param name="updatedTicket">The updated ticket.</param>
        /// <exception cref="ArgumentNullException">Thrown when updatedTicket is null.</exception>
        void UpdateTicket(Ticket? updatedTicket);

        /// <summary>
        /// Deletes a specific ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to delete.</param>
        void DeleteTicket(int id);
    }

    /// <summary>
    /// Provides operations for ticket management.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly List<Ticket?> _tickets = new List<Ticket?>();
        private int _nextTicketId = 1;

        /// <summary>
        /// Retrieves all tickets.
        /// </summary>
        /// <returns>A collection of all tickets.</returns>
        public IEnumerable<Ticket?> GetAllTickets()
        {
            return _tickets;
        }

        /// <summary>
        /// Retrieves a specific ticket by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket.</param>
        /// <returns>The ticket if found; otherwise, null.</returns>
        public Ticket? GetTicketById(int id)
        {
            return _tickets.Find(ticket => ticket != null && ticket.TicketId == id);
        }

        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="ticket">The ticket to create.</param>
        /// <returns>The created ticket with assigned ID.</returns>
        /// <exception cref="ArgumentNullException">Thrown when ticket is null.</exception>
        public Ticket? CreateTicket(Ticket? ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            ticket.TicketId = _nextTicketId++;
            _tickets.Add(ticket);

            return ticket;
        }

        /// <summary>
        /// Updates a specific ticket.
        /// </summary>
        /// <param name="updatedTicket">The updated ticket.</param>
        /// <exception cref="ArgumentNullException">Thrown when updatedTicket is null.</exception>
        public void UpdateTicket(Ticket? updatedTicket)
        {
            if (updatedTicket == null)
            {
                throw new ArgumentNullException(nameof(updatedTicket));
            }

            var index = _tickets.FindIndex(ticket => ticket != null && ticket.TicketId == updatedTicket.TicketId);
            if (index != -1)
            {
                _tickets[index] = updatedTicket;
            }
        }

        /// <summary>
        /// Deletes a specific ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to delete.</param>
        public void DeleteTicket(int id)
        {
            _tickets.RemoveAll(ticket => ticket != null && ticket.TicketId == id);
        }
    }
}