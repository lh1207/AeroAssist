using AeroAssist.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AeroAssist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Ticket")]
        public ActionResult<IEnumerable<Ticket>> Get()
        {
            var tickets = _ticketService.GetAllTickets();
            return Ok(tickets);
        }

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
