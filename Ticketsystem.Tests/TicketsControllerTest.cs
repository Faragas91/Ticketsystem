using System.Net;
using Xunit;

public class TicketsControllerTest
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TicketsControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Tickets.Add(new Ticket
        {
            Title = "Test Ticket",
            Description = "Test Beschreibung",
        });

        context.SaveChanges();
    }

    [Fact]
    public async Task GetTickets_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/tickets");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
