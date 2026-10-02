using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;
using CineBook.Data;



namespace CineBook.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public   ApplicationUser User { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;


        public int Rating { get; set; }
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    }
}
