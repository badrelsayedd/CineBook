using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CineBook.Data;


namespace CineBook.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string BookingNumber { get; set; } = string.Empty;


        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;


        public int ShowTimeId { get; set; }
        public ShowTime ShowTime { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CancelledAt { get; set; }



        public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();



    }
}
