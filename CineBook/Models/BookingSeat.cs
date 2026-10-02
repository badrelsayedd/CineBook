using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;



namespace CineBook.Models
{
    public class BookingSeat
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public int SeatId { get; set; }
        public Seat Seat { get; set; } = null!;

        public decimal PriceAtBooking { get; set; }
    }
}
