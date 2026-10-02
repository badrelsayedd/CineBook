using CineBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CineBook.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<Director> Directors => Set<Director>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<MovieActor> MovieActors => Set<MovieActor>();
        public DbSet<MovieDirector> MovieDirectors => Set<MovieDirector>();
        public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
        public DbSet<Cinema> Cinemas => Set<Cinema>();
        public DbSet<Screen> Screens => Set<Screen>();
        public DbSet<SeatType> SeatTypes => Set<SeatType>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<ShowTime> ShowTimes => Set<ShowTime>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingSeat> BookingSeats => Set<BookingSeat>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Actors Constraints
            modelBuilder.Entity<Actor>(e =>
            {
                e.Property(x => x.FullName).IsRequired().HasMaxLength(150);
                e.Property(x => x.ProfileImageUrl).HasMaxLength(500);
                e.Property(x => x.Biography).HasMaxLength(2000);
                e.HasIndex(x => x.FullName);
            });



            // Directors Constraints
            modelBuilder.Entity<Director>(e =>
            {
                e.Property(x => x.FullName).IsRequired().HasMaxLength(150);
                e.Property(x => x.ProfileImageUrl).HasMaxLength(500);
                e.Property(x => x.Biography).HasMaxLength(2000);
                e.HasIndex(x => x.FullName);
            });


            // Genres Constraints
            modelBuilder.Entity<Genre>(e =>
            {
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x => x.Description).HasMaxLength(500);
                e.HasIndex(x => x.Name).IsUnique();
            });


            // Movies Constraints
            modelBuilder.Entity<Movie>(e =>
            {
                e.Property(x => x.Title).IsRequired().HasMaxLength(200);
                e.Property(x => x.Description).IsRequired().HasMaxLength(2000);
                e.Property(x => x.PosterUrl).HasMaxLength(500);
                e.Property(x => x.TrailerUrl).HasMaxLength(500);
                e.Property(x => x.AgeRating).HasMaxLength(10);

                e.HasIndex(x => x.Title);
                e.HasIndex(x => new { x.IsActive, x.ReleaseDate });
                e.ToTable(t => t.HasCheckConstraint("CK_Movie_Duration", "[DurationInMinutes] >= 0"));
            });

            // MovieGenre Constraints & Relationships (Many-to-Many)
            modelBuilder.Entity<MovieGenre>(e =>
            {
                e.HasOne(x => x.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(x => new { x.MovieId, x.GenreId }).IsUnique();
            
            });


            // MovieActor Constraints & Relationships (Many-to-Many)
            modelBuilder.Entity<MovieActor>(e =>
            {
                e.Property(x => x.CharacterName).HasMaxLength(100);

                e.HasOne(x => x.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(x => x.ActorId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // MovieDirector Constraints & Relationships (Many-to-Many)
            modelBuilder.Entity<MovieDirector>(e =>
            {
                e.HasOne(x => x.Movie)
                .WithMany(m => m.MovieDirectors)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Director)
                .WithMany(d => d.MovieDirectors)
                .HasForeignKey(x => x.DirectorId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // Cinema Constraints
            modelBuilder.Entity<Cinema>(e =>
            {
                e.Property(x => x.Name).IsRequired().HasMaxLength(150);
                e.Property(x => x.Location).IsRequired().HasMaxLength(300);
                e.HasIndex(x => new { x.Name, x.Location }).IsUnique();
            
            });

            // Screen Constraints and Relationship with Cinema
            modelBuilder.Entity<Screen>(e =>
            {
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.HasIndex(x => new { x.CinemaId, x.Name }).IsUnique();

                e.HasOne(x => x.Cinema)
                .WithMany(c => c.Screens)
                .HasForeignKey(x => x.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // SeatType Constraints
            modelBuilder.Entity<SeatType>(e =>
            {
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x => x.PriceMultiplier).IsRequired().HasColumnType("decimal(5,2)");
                e.HasIndex(x => x.Name).IsUnique();
                e.ToTable(x => x.HasCheckConstraint("CK_SeatType_PriceMultiplier", "[PriceMultiplier] >= 0"));

                // Seed
                e.HasData(
                    new SeatType { Id = 1, Name = "Standard", PriceMultiplier = 1.0m },
                    new SeatType { Id = 2, Name = "VIP", PriceMultiplier = 1.5m }
                );

            });

            // Seat Constraints and Relationship with Screen and SeatType
            modelBuilder.Entity<Seat>(e =>
            {
                e.Property(x => x.RowName).IsRequired().HasMaxLength(10);
                e.Property(x => x.SeatNumber).IsRequired();
                e.HasIndex(x => new { x.ScreenId, x.RowName, x.SeatNumber }).IsUnique();

               e.HasOne(x => x.Screen)
                .WithMany(s => s.Seats)
                .HasForeignKey(x => x.ScreenId)
                .OnDelete(DeleteBehavior.Restrict);


                e.HasOne(x => x.SeatType)
                .WithMany(st => st.Seats)
                .HasForeignKey(x => x.SeatTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // ShowTime Constraints
            modelBuilder.Entity<ShowTime>(e =>
            {
                e.Property(x => x.TicketPrice).IsRequired().HasPrecision(18, 2);

                e.HasOne(x => x.Movie)
                .WithMany(m => m.ShowTimes)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Screen)
                .WithMany(s => s.ShowTimes)
                .HasForeignKey(x => x.ScreenId)
                .OnDelete(DeleteBehavior.Restrict);

                e.Property(x => x.StartTime).IsRequired();
                e.Property(x => x.EndTime).IsRequired();
                e.HasIndex(x => new { x.ScreenId, x.StartTime }).IsUnique();
                e.HasIndex(x => new { x.MovieId, x.StartTime });
                e.ToTable(t => {
                    t.HasCheckConstraint("CK_ShowTime_EndTime", "[EndTime] > [StartTime]");
                    t.HasCheckConstraint("CK_ShowTime_TicketPrice", "[TicketPrice] >= 0");
                });
                    
            });

            // Booking Constraints and Relationship with User and ShowTime
            modelBuilder.Entity<Booking>(e =>
            {
                e.Property(x => x.BookingNumber).IsRequired().HasMaxLength(50);
                e.Property(x => x.TotalAmount).IsRequired().HasPrecision(18, 2);

                e.HasOne(x => x.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.ShowTime)
                .WithMany(st => st.Bookings)
                .HasForeignKey(x => x.ShowTimeId)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => x.BookingNumber).IsUnique();
                e.HasIndex(x => new { x.UserId, x.CreatedAt });
                e.HasIndex(x => new { x.ShowTimeId, x.Status });

                e.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Booking_TotalAmount", "[TotalAmount] >= 0");
                });
            });

            // BookingSeat Constraints
            modelBuilder.Entity<BookingSeat>(e =>
            {
                e.Property(x => x.PriceAtBooking).IsRequired().HasPrecision(18, 2);

                e.HasOne(x => x.Booking)
                .WithMany(b => b.BookingSeats)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);


                e.HasOne(x => x.Seat)
                .WithMany(s => s.BookingSeats)
                .HasForeignKey(x => x.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.ShowTime)
                .WithMany(st => st.BookingSeats)
                .HasForeignKey(x => x.ShowTimeId)
                .OnDelete(DeleteBehavior.Restrict);


                e.HasIndex(x => new { x.ShowTimeId, x.SeatId }).IsUnique();

                e.ToTable(t => t.HasCheckConstraint("CK_BookingSeat_Price", "[PriceAtBooking] >= 0"));

            });

            // Review Constraints and Relationship with User and Movie
            modelBuilder.Entity<Review>(e =>
            {
                e.Property(x => x.Comment).HasMaxLength(1000);

                e.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Movie)
                    .WithMany(m => m.Reviews)
                    .HasForeignKey(x => x.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(x => new { x.UserId, x.MovieId }).IsUnique();   // مراجعة واحدة لكل يوزر لكل فيلم

                e.ToTable(t => t.HasCheckConstraint("CK_Review_Rating", "[Rating] BETWEEN 1 AND 5"));
            });

            // Seed: Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "a1b2c3d4-0000-0000-0000-000000000001",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "role-admin-stamp"
                },
                new IdentityRole
                {
                    Id = "a1b2c3d4-0000-0000-0000-000000000002",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "role-user-stamp"
                }
            );
        }
    }
}
