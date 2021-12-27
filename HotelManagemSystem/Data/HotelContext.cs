using HotelManagemSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HotelManagemSystem.Data
{
    public class HotelContext : IdentityDbContext<User>
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().ToTable("Room");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<RoomType>().ToTable("RoomType");

            modelBuilder.Entity<RoomType>().HasData(
                new RoomType { RoomTypeId = 1, Name = "Single Room" },
                new RoomType { RoomTypeId = 2, Name = "Standard Double Room" },
                new RoomType { RoomTypeId = 3, Name = "Standard Family Room" },
                new RoomType { RoomTypeId = 4, Name = "Garden Family Room" },
                new RoomType { RoomTypeId = 5, Name = "Deluxe Double Room" },
                new RoomType { RoomTypeId = 6, Name = "Executive Junior Suite" },
                new RoomType { RoomTypeId = 7, Name = "Maisonette" }
                );
            base.OnModelCreating(modelBuilder);
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "123456";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
