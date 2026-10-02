using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;


namespace CineBook.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Biography { get; set; }


        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
