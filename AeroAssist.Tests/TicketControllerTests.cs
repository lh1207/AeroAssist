using AeroAssist.Controllers;
using AeroAssist.Data.Models;
using AeroAssist.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace AeroAssist.Tests;

public class TicketControllerTests
{
    private readonly Mock<TicketService.ITicketService> _mockService;
    private readonly TicketController _controller;

    public TicketControllerTests()
    {
        _mockService = new Mock<TicketService.ITicketService>();
        _controller = new TicketController(_mockService.Object, NullLogger<TicketModel>.Instance);
    }

    // Constructor

    [Fact]
    public void Constructor_WithNullService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => new TicketController(null!, NullLogger<TicketModel>.Instance));
    }

    // GET all

    [Fact]
    public void Get_ReturnsOkWithAllTickets()
    {
        var tickets = new[] { new Ticket { Title = "A" }, new Ticket { Title = "B" } };
        _mockService.Setup(s => s.GetAllTickets()).Returns(tickets);

        var result = _controller.Get();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Same(tickets, ok.Value);
    }

    [Fact]
    public void Get_WhenEmpty_ReturnsOkWithEmptyCollection()
    {
        _mockService.Setup(s => s.GetAllTickets()).Returns([]);

        var result = _controller.Get();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Empty((IEnumerable<Ticket?>)ok.Value!);
    }

    // GET by id

    [Fact]
    public void GetById_WhenFound_ReturnsOkWithTicket()
    {
        var ticket = new Ticket { TicketId = 1, Title = "Found" };
        _mockService.Setup(s => s.GetTicketById(1)).Returns(ticket);

        var result = _controller.Get(1);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Same(ticket, ok.Value);
    }

    [Fact]
    public void GetById_WhenNotFound_ReturnsNotFound()
    {
        _mockService.Setup(s => s.GetTicketById(99)).Returns((Ticket?)null);

        var result = _controller.Get(99);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    // POST

    [Fact]
    public void Post_WithNullTicket_ReturnsBadRequest()
    {
        var result = _controller.Post(null);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public void Post_WithValidTicket_ReturnsCreatedAtAction()
    {
        var ticket = new Ticket { TicketId = 1, Title = "New" };
        _mockService.Setup(s => s.CreateTicket(ticket)).Returns(ticket);

        var result = _controller.Post(ticket);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Same(ticket, created.Value);
        Assert.Equal(nameof(_controller.Get), created.ActionName);
        Assert.Equal(ticket.TicketId, ((dynamic)created.RouteValues!["id"]!));
    }

    [Fact]
    public void Post_WhenServiceReturnsNull_Returns500()
    {
        var ticket = new Ticket { Title = "Failing" };
        _mockService.Setup(s => s.CreateTicket(ticket)).Returns((Ticket?)null);

        var result = _controller.Post(ticket);

        var status = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, status.StatusCode);
    }

    // PUT

    [Fact]
    public void Put_WithNullTicket_ReturnsBadRequest()
    {
        var result = _controller.Put(1, null);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Put_WithMismatchedId_ReturnsBadRequest()
    {
        var ticket = new Ticket { TicketId = 2, Title = "X" };

        var result = _controller.Put(1, ticket);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Put_WhenTicketNotFound_ReturnsNotFound()
    {
        var ticket = new Ticket { TicketId = 1, Title = "X" };
        _mockService.Setup(s => s.GetTicketById(1)).Returns((Ticket?)null);

        var result = _controller.Put(1, ticket);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Put_WhenValid_ReturnsNoContentAndCallsUpdate()
    {
        var ticket = new Ticket { TicketId = 1, Title = "Updated" };
        _mockService.Setup(s => s.GetTicketById(1)).Returns(ticket);

        var result = _controller.Put(1, ticket);

        Assert.IsType<NoContentResult>(result);
        _mockService.Verify(s => s.UpdateTicket(ticket), Times.Once);
    }

    // DELETE

    [Fact]
    public void Delete_WhenNotFound_ReturnsNotFound()
    {
        _mockService.Setup(s => s.GetTicketById(99)).Returns((Ticket?)null);

        var result = _controller.Delete(99);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_WhenFound_ReturnsNoContentAndCallsDelete()
    {
        var ticket = new Ticket { TicketId = 1, Title = "Target" };
        _mockService.Setup(s => s.GetTicketById(1)).Returns(ticket);

        var result = _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
        _mockService.Verify(s => s.DeleteTicket(1), Times.Once);
    }
}
