using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.ComponentModel.DataAnnotations;

namespace CineBook.Models
{
    public enum BookingStatus
    {
        Confirmed = 1,
        Cancelled = 2,
        Completed = 3
    }
}
