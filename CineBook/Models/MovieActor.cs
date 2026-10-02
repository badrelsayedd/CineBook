using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;


namespace CineBook.Models
{
    public class MovieActor
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;


        public int ActorId { get; set; }
        public Actor Actor { get; set; } = null!;


        public string? CharacterName { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
