using System;
using System.Collections.Generic;
using AeroAssist.Services;
using Microsoft.AspNetCore.Mvc;

namespace AeroAssist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
        }

        // GET: api/Ticket
        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> Get()
        {
            var tickets = _ticketService.GetAllTickets();
            return Ok(tickets);
        }

        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public ActionResult<Ticket> Get(int id)
        {
            var ticket = _ticketService.GetTicketById(id);

            if (ticket == null) // TODO: Handle nulls
            {
                return NotFound(); // 404
            }

            return Ok(ticket);
        }

        // POST: api/Ticket
        [HttpPost]
        public ActionResult<Ticket> Post([FromBody] Ticket ticket)
        {
            if (ticket == null) // TODO: Handle nulls
            {
                return BadRequest(); // 400
            }

            var createdTicket = _ticketService.CreateTicket(ticket);
            return CreatedAtAction(nameof(Get), new { id = createdTicket.TicketId }, createdTicket);
        }

        // PUT: api/Ticket/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ticket updatedTicket)
        {
            if (updatedTicket == null || id != updatedTicket.TicketId) // TODO: Handle nulls
            {
                return BadRequest(); // 400
            }

            var existingTicket = _ticketService.GetTicketById(id);

            if (existingTicket == null) // TODO: Handle nulls
            {
                return NotFound(); // 404
            }

            _ticketService.UpdateTicket(updatedTicket);

            return NoContent(); // 204
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingTicket = _ticketService.GetTicketById(id);

            if (existingTicket == null) // TODO: Handle nulls
            {
                return NotFound(); // 404
            }

            _ticketService.DeleteTicket(id);

            return NoContent(); // 204
        }
    }
}