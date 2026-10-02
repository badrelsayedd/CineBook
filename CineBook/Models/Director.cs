using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;



namespace CineBook.Models
{
    public class Director
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string? ProfileImageUrl { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Biography { get; set; }


        public ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
    }
}
