using Microsoft.EntityFrameworkCore;
using DisasterManagement.Models;


namespace DisasterManagement.Data
{        
    public class DisasterContext : DbContext
    {
        public DisasterContext(DbContextOptions<DisasterContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Crisis> Crises { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Models.Task> Tasks { get; set;}         
        public DbSet<VolunteerCrisis> VolunteerCrises { get; set; }

    }
    
}