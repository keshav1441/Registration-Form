using Microsoft.EntityFrameworkCore;
using UserReg.Models;

namespace UserReg.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map the User entity to the userregistration table
            modelBuilder.Entity<User>()
                .ToTable("userregistration");
        }
    }
}
