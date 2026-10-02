using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;


namespace CineBook.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public int ScreenId { get; set; }
        public Screen Screen { get; set; } = null!;

        public int SeatTypeId { get; set; }
        public SeatType SeatType { get; set; } = null!;


        public int SeatNumber { get; set; }
        public string RowName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;


        public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();



    }
}
