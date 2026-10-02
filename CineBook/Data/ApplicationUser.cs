using CineBook.Models;
using Microsoft.AspNetCore.Identity;

namespace CineBook.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;


        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
