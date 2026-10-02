using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;


namespace CineBook.Models
{
    public class MovieDirector
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;


        public int DirectorId { get; set; }
        public Director Director { get; set; } = null!;
    }
}
