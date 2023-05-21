using Microsoft.EntityFrameworkCore;
using Lokaverkefni.Models;
using System.Text.RegularExpressions;

namespace Lokaverkefni.Data
{
    public class TwitterDbContext : DbContext
    {

        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=TwitterDb;Trusted_Connection=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

                modelBuilder.Entity<User>()
                    .Property(b => b.ProfilePicture)
                    .HasDefaultValue("https://localhost:7060/images/Default.jpg");

                modelBuilder.Entity<User>()
                    .HasIndex(u => u.Name)
                    .IsUnique();

            modelBuilder.Entity<User>().HasMany(u => u.Following)
                            .WithMany(u => u.Followers).UsingEntity<Dictionary<string, object>>("UserFollows", b => b.HasOne<User>()
                            .WithMany().HasForeignKey("FollowerId").OnDelete(DeleteBehavior.Restrict), b => b.HasOne<User>().WithMany()
                            .HasForeignKey("FollowingId").OnDelete(DeleteBehavior.Restrict));

            User u1 = new User { UserId = 1, Name = "Taurus", Password = "Realpassword", Email = "The@Email.com", PhoneNumber = 11111, ProfilePicture = "https://localhost:7060/images/Ghost.png"};
            User u2 = new User { UserId = 2, Name = "JimmyJames", Password = "Notpassword", Email = "The@TrueEmail.com", PhoneNumber = 22222 };
            User u3 = new User { UserId = 3, Name = "GimmyGames", Password = "Password", Email = "The@NotEmail.com", PhoneNumber = 33333 };

            modelBuilder.Entity<User>().HasData(u1);
            modelBuilder.Entity<User>().HasData(u2);
            modelBuilder.Entity<User>().HasData(u3);

            modelBuilder.Entity("UserFollows").HasData(
                new { FollowerId = u1.UserId, FollowingId = u2.UserId }, 
                new { FollowerId = u1.UserId, FollowingId = u3.UserId }, 
                new { FollowerId = u3.UserId, FollowingId = u1.UserId });


            Post p1 = new Post { PostId = 1, Text = "Taurus", Image = "https://localhost:7060/images/Ghost.png", UserId = 1 };
            Post p2 = new Post { PostId = 2, Text = "Why Is this the default Profile Picture??", Image = "https://localhost:7060/images/Default.jpg", UserId = 3 };
            Post p3 = new Post { PostId = 3, Text = "I don't like posting Images", UserId = 2 };

            modelBuilder.Entity<Post>().HasData(p1);
            modelBuilder.Entity<Post>().HasData(p2);
            modelBuilder.Entity<Post>().HasData(p3);

        }

    }
}
