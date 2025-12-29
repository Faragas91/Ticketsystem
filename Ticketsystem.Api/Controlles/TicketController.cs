using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticketsystem.Api.Data;
using Ticketsystem.Api.Models;
using Ticketsystem.Api.DTOs;

namespace Ticketsystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _context.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                CreatedByUserId = t.CreatedByUserId
            }).ToListAsync();

            return Ok(tickets);
        }

        // POST: api/tickets
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(TicketCreateDto dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = TicketStatus.Open,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = 1 // Placeholder for the user ID, should be replaced with actual user context
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            var ticketDto = new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                CreatedAt = ticket.CreatedAt,
                CreatedByUserId = ticket.CreatedByUserId
            };

            return CreatedAtAction(nameof(GetTickets), new { id = ticket.Id }, ticket);
        }

        // PUT: api/tickets/{id}/status
        [HttpPut("{id}/status")]
        public async Task<ActionResult<TicketDto>> UpdateStatus(int id, [FromQuery] TicketStatus newStatus, [FromQuery] UserRole role)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Only Admins can change the status to Closed or Approved
            if (role != UserRole.Admin)
            {
                return Forbid("Only admins can change the ticket status to Closed.");
            }
            ticket.Status = newStatus;
            await _context.SaveChangesAsync();

            var ticketDto = new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                CreatedAt = ticket.CreatedAt,
                CreatedByUserId = ticket.CreatedByUserId
            };

            return Ok(ticketDto);
        }
    }
}
