using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;


namespace CineBook.Models
{
    public class SeatType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal PriceMultiplier { get; set; }
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
