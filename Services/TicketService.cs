using System;
using System.Collections.Generic;

namespace AeroAssist.Services
{
    public interface ITicketService
    {
        IEnumerable<Ticket> GetAllTickets();
        Ticket GetTicketById(int id);
        Ticket CreateTicket(Ticket ticket);
        void UpdateTicket(Ticket updatedTicket);
        void DeleteTicket(int id);
    }

    public class TicketService : ITicketService
    {
        private readonly List<Ticket> _tickets = new List<Ticket>();
        private int _nextTicketId = 1;

        public IEnumerable<Ticket> GetAllTickets()
        {
            return _tickets;
        }

        public Ticket GetTicketById(int id)
        {
            return _tickets.Find(ticket => ticket.TicketId == id);
        }

        public Ticket CreateTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            ticket.TicketId = _nextTicketId++;
            _tickets.Add(ticket);

            return ticket;
        }

        public void UpdateTicket(Ticket updatedTicket)
        {
            if (updatedTicket == null)
            {
                throw new ArgumentNullException(nameof(updatedTicket));
            }

            var index = _tickets.FindIndex(ticket => ticket.TicketId == updatedTicket.TicketId);
            if (index != -1)
            {
                _tickets[index] = updatedTicket;
            }
        }

        public void DeleteTicket(int id)
        {
            _tickets.RemoveAll(ticket => ticket.TicketId == id);
        }
    }
}