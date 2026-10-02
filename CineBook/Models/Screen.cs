using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;

namespace CineBook.Models
{
    public class Screen
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;


        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();



    }
}
