using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Ticketsystem.Tests;

public class TicketsControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TicketsControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTickets_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/tickets");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}