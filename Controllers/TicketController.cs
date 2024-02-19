using AeroAssist.Services;
using Microsoft.AspNetCore.Mvc;

namespace AeroAssist.Controllers
{
    /// <summary>
    /// Handles HTTP requests related to tickets.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketController"/> class.
        /// </summary>
        /// <param name="ticketService">The ticket service.</param>
        /// <exception cref="ArgumentNullException">Thrown when ticketService is null.</exception>
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
        }

        /// <summary>
        /// Retrieves all tickets from the service and returns them.
        /// </summary>
        [HttpGet("Ticket")]
        public ActionResult<IEnumerable<Ticket>> Get()
        {
            var tickets = _ticketService.GetAllTickets();
            return Ok(tickets);
        }

        /// <summary>
        /// Retrieves a specific ticket by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket.</param>
        [HttpGet("{id}")]
        public ActionResult<Ticket> Get(int id)
        {
            var ticket = _ticketService.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="ticket">The ticket to create.</param>
        [HttpPost]
        public ActionResult<Ticket> Post([FromBody] Ticket? ticket)
        {
            if (ticket == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTicket = _ticketService.CreateTicket(ticket);

            if (createdTicket != null)
            {
                return CreatedAtAction(nameof(Get), new { id = createdTicket.TicketId }, createdTicket);
            }
            else
            {
                return StatusCode(500, "Failed to create ticket");
            }
        }

        /// <summary>
        /// Updates a specific ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to update.</param>
        /// <param name="updatedTicket">The updated ticket.</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ticket? updatedTicket)
        {
            if (updatedTicket == null || id != updatedTicket.TicketId || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTicket = _ticketService.GetTicketById(id);

            if (existingTicket == null)
            {
                return NotFound();
            }

            _ticketService.UpdateTicket(updatedTicket);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingTicket = _ticketService.GetTicketById(id);

            if (existingTicket == null)
            {
                return NotFound();
            }

            _ticketService.DeleteTicket(id);

            return NoContent();
        }
    }
}