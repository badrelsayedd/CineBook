using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;

namespace CineBook.Models
{
    public class MovieGenre
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
