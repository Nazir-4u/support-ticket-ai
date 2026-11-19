using System.ComponentModel.DataAnnotations;
using Xunit;

public class TicketServiceTests
{
    [Fact]
    public void CreateTicket_WithValidData_ReturnsTicketWithOpenStatus()
    {
        var service = new TicketService(new TicketRepository());

        var result = service.CreateTicket(new TicketDto
        {
            Title = "Login issue",
            Description = "Cannot log in after password reset."
        });

        Assert.NotEqual(0, result.Id);
        Assert.Equal("Login issue", result.Title);
        Assert.Equal("Cannot log in after password reset.", result.Description);
        Assert.Equal("Open", result.Status);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateTicket_WithInvalidTitle_ThrowsValidationException(string? title)
    {
        var service = new TicketService(new TicketRepository());

        var dto = new TicketDto
        {
            Title = title ?? string.Empty,
            Description = "Application crashes on startup."
        };

        var exception = Assert.Throws<ValidationException>(() => service.CreateTicket(dto));
        Assert.Equal("Title is required.", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateTicket_WithInvalidDescription_ThrowsValidationException(string? description)
    {
        var service = new TicketService(new TicketRepository());

        var dto = new TicketDto
        {
            Title = "Printer not working",
            Description = description ?? string.Empty
        };

        var exception = Assert.Throws<ValidationException>(() => service.CreateTicket(dto));
        Assert.Equal("Description is required.", exception.Message);
    }

    [Fact]
    public void CreateTicket_GeneratesUniqueIdsForEachTicket()
    {
        var service = new TicketService(new TicketRepository());

        var first = service.CreateTicket(new TicketDto
        {
            Title = "Dashboard issue",
            Description = "The dashboard loads blank."
        });

        var second = service.CreateTicket(new TicketDto
        {
            Title = "Export issue",
            Description = "CSV export fails."
        });

        Assert.NotEqual(first.Id, second.Id);
        Assert.True(first.Id > 0);
        Assert.True(second.Id > 0);
    }
}
