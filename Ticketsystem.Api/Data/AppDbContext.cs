using Microsoft.EntityFrameworkCore;
using Ticketsystem.Api.Models;

namespace Ticketsystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; } = null!;
    }
}