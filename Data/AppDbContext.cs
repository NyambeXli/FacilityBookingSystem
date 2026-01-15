using UfsConnectBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UfsConnectBook.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UfsConnectBook.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Catagory> Category { get; set; }
        public DbSet<Models.Entities.Facility> Facilities { get; set; }
    }
}
