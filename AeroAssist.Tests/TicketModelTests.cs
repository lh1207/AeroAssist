using AeroAssist.Data;
using AeroAssist.Data.Models;
using Microsoft.AspNetCore.Http;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace AeroAssist.Tests;

public class TicketModelTests : IDisposable
{
    private readonly AeroAssistContext _context;
    private readonly TicketModel _model;

    public TicketModelTests()
    {
        var options = new DbContextOptionsBuilder<AeroAssistContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new AeroAssistContext(options);

        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(
            mockUserStore.Object, null, null, null, null, null, null, null, null);

        var mockConfig = new Mock<IConfiguration>();

        _model = new TicketModel(
            mockUserManager.Object,
            NullLogger<TicketModel>.Instance,
            _context,
            mockConfig.Object);

        // Minimal PageContext so IActionResult helpers (Page(), NotFound(), etc.) work
        var httpContext = new DefaultHttpContext();
        _model.PageContext = new PageContext
        {
            HttpContext = httpContext,
            RouteData = new RouteData(),
            ActionDescriptor = new CompiledPageActionDescriptor(),
        };
        _model.Url = new UrlHelper(new ActionContext(
            httpContext,
            new RouteData(),
            new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()));
    }

    public void Dispose() => _context.Dispose();

    // ---- OnPut ----

    [Fact]
    public async Task OnPut_WhenTicketNotFound_ReturnsNotFound()
    {
        var result = await _model.OnPut(999, new Ticket { Title = "X" });

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnPut_WhenFound_UpdatesFieldsAndRedirectsToSuccess()
    {
        var ticket = new Ticket { Title = "Old Title", Status = "Open" };
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        var updated = new Ticket
        {
            TicketId = ticket.TicketId,
            Title = "New Title",
            Status = "Closed",
            Due = ticket.Due,
        };

        var result = await _model.OnPut(ticket.TicketId, updated);

        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Success", redirect.PageName);

        var saved = await _context.Tickets.FindAsync(ticket.TicketId);
        Assert.Equal("New Title", saved!.Title);
        Assert.Equal("Closed", saved.Status);
    }

    [Fact]
    public async Task OnPut_WhenDueDateChanges_UpdatesDueDate()
    {
        var originalDue = new DateTime(2026, 6, 1);
        var ticket = new Ticket { Title = "T", Due = originalDue };
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        var newDue = new DateTime(2026, 7, 1);
        var updated = new Ticket { TicketId = ticket.TicketId, Title = "T", Due = newDue };

        await _model.OnPut(ticket.TicketId, updated);

        var saved = await _context.Tickets.FindAsync(ticket.TicketId);
        Assert.Equal(newDue, saved!.Due);
    }

    [Fact]
    public async Task OnPut_WhenDueDateUnchanged_PreservesDueDate()
    {
        var due = new DateTime(2026, 6, 1);
        var ticket = new Ticket { Title = "T", Due = due };
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        var updated = new Ticket { TicketId = ticket.TicketId, Title = "T", Due = due };

        await _model.OnPut(ticket.TicketId, updated);

        var saved = await _context.Tickets.FindAsync(ticket.TicketId);
        Assert.Equal(due, saved!.Due);
    }

    // ---- OnDelete ----

    [Fact]
    public async Task OnDelete_WhenTicketNotFound_ReturnsNotFound()
    {
        var result = await _model.OnDelete(999, new Ticket { Title = "X" });

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnDelete_WhenFound_RemovesTicketAndRedirectsToTicketPage()
    {
        var ticket = new Ticket { Title = "To Delete" };
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        var result = await _model.OnDelete(ticket.TicketId, ticket);

        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Ticket", redirect.PageName);
        Assert.Empty(_context.Tickets);
    }

    [Fact]
    public async Task OnDelete_WhenFound_DoesNotAffectOtherTickets()
    {
        var keep = new Ticket { Title = "Keep" };
        var remove = new Ticket { Title = "Remove" };
        _context.Tickets.AddRange(keep, remove);
        await _context.SaveChangesAsync();

        await _model.OnDelete(remove.TicketId, remove);

        var remaining = await _context.Tickets.ToListAsync();
        Assert.Single(remaining);
        Assert.Equal("Keep", remaining[0].Title);
    }
}
