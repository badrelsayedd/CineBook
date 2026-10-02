using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;

namespace CineBook.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? AgeRating { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
    }
}
