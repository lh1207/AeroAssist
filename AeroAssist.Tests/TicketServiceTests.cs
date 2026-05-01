using AeroAssist.Data;
using AeroAssist.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace AeroAssist.Tests;

public class TicketServiceTests : IDisposable
{
    private readonly AeroAssistContext _context;
    private readonly TicketService _service;

    public TicketServiceTests()
    {
        var options = new DbContextOptionsBuilder<AeroAssistContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new AeroAssistContext(options);
        _service = new TicketService(_context, NullLogger<TicketService>.Instance);
    }

    public void Dispose() => _context.Dispose();

    // GetAllTickets

    [Fact]
    public void GetAllTickets_WhenEmpty_ReturnsEmptyCollection()
    {
        var result = _service.GetAllTickets();

        Assert.Empty(result);
    }

    [Fact]
    public void GetAllTickets_ReturnsAllTickets()
    {
        _context.Tickets.AddRange(
            new Ticket { Title = "Alpha" },
            new Ticket { Title = "Beta" });
        _context.SaveChanges();

        var result = _service.GetAllTickets();

        Assert.Equal(2, result.Count());
    }

    // GetTicketById

    [Fact]
    public void GetTicketById_WhenFound_ReturnsTicket()
    {
        var ticket = new Ticket { Title = "Found" };
        _context.Tickets.Add(ticket);
        _context.SaveChanges();

        var result = _service.GetTicketById(ticket.TicketId);

        Assert.NotNull(result);
        Assert.Equal("Found", result.Title);
    }

    [Fact]
    public void GetTicketById_WhenNotFound_ReturnsNull()
    {
        var result = _service.GetTicketById(999);

        Assert.Null(result);
    }

    // CreateTicket

    [Fact]
    public void CreateTicket_WithValidTicket_PersistsAndReturnsTicket()
    {
        var ticket = new Ticket { Title = "New Ticket" };

        var result = _service.CreateTicket(ticket);

        Assert.NotNull(result);
        Assert.Equal("New Ticket", result.Title);
        Assert.Single(_context.Tickets);
    }

    [Fact]
    public void CreateTicket_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _service.CreateTicket(null));
    }

    [Fact]
    public void CreateTicket_AssignsIdAfterPersist()
    {
        var ticket = new Ticket { Title = "Check Id" };

        _service.CreateTicket(ticket);

        Assert.NotEqual(0, ticket.TicketId);
    }

    // UpdateTicket

    [Fact]
    public void UpdateTicket_WithValidTicket_PersistsChanges()
    {
        var ticket = new Ticket { Title = "Original" };
        _context.Tickets.Add(ticket);
        _context.SaveChanges();
        _context.Entry(ticket).State = EntityState.Detached;

        ticket.Title = "Updated";
        _service.UpdateTicket(ticket);

        var saved = _context.Tickets.Find(ticket.TicketId);
        Assert.Equal("Updated", saved!.Title);
    }

    [Fact]
    public void UpdateTicket_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _service.UpdateTicket(null));
    }

    // DeleteTicket

    [Fact]
    public void DeleteTicket_WhenFound_RemovesTicket()
    {
        var ticket = new Ticket { Title = "To Delete" };
        _context.Tickets.Add(ticket);
        _context.SaveChanges();

        _service.DeleteTicket(ticket.TicketId);

        Assert.Empty(_context.Tickets);
    }

    [Fact]
    public void DeleteTicket_WhenNotFound_DoesNotThrow()
    {
        var exception = Record.Exception(() => _service.DeleteTicket(999));

        Assert.Null(exception);
    }

    [Fact]
    public void DeleteTicket_WhenNotFound_LeavesOtherTicketsIntact()
    {
        _context.Tickets.Add(new Ticket { Title = "Keep Me" });
        _context.SaveChanges();

        _service.DeleteTicket(999);

        Assert.Single(_context.Tickets);
    }
}
