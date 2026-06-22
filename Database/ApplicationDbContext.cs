
using Microsoft.EntityFrameworkCore;
using incidentmanagement.Models;

namespace incidentmanagement.Database

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
         public DbSet<Incident> Incidents { get; set; }
         public DbSet<Comment> Comments { get; set; }
   
         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);
            // Configure relationships and constraints here if needed
        }
    
    }
}
