using Microsoft.EntityFrameworkCore;
using Workout_API.Models;

namespace Workout_API.DBContexts
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<WorkingSet> WorkingSets { get; set; }
        public DbSet<WarmupSet> WarmupSets { get; set; }
        public DbSet<BodyPart> BodyParts { get; set; }
        public DbSet<MovementPattern> MovementsPatterns { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
