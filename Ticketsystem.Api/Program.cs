using Microsoft.EntityFrameworkCore;
using Ticketsystem.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();   
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core InMemory DB registrieren
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TicketsystemDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
