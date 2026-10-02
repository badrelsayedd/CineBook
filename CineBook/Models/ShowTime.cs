using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;


namespace CineBook.Models
{
    public class ShowTime
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;


        public int ScreenId { get; set; }
        public Screen Screen { get; set; } = null!;


        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public decimal TicketPrice { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
