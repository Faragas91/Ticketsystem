// DTO for transferring ticket data 
// backend to frontend
// includes all relevant ticket information

using Ticketsystem.Api.Models;

namespace Ticketsystem.Api.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedByUserId { get; set; }
    }
}