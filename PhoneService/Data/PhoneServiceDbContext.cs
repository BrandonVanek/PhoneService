using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoneService.Models;

namespace PhoneService.Data
{
    public class PhoneServiceDbContext : DbContext
    {
        public PhoneServiceDbContext(DbContextOptions<PhoneServiceDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<UserPlan> UserPlans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPlan>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<UserPlan>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserPlans)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserPlan>()
                .HasOne(pt => pt.Plan)
                .WithMany(t => t.UserPlans)
                .HasForeignKey(pt => pt.PlanId);

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<PhoneNumber>().HasIndex(u => u.Number).IsUnique();
        }
    }
}
