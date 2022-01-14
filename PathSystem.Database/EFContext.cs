using Microsoft.EntityFrameworkCore;
using PathSystem.Database.Deserializer;
using PathSystem.Models;
using PathSystem.Models.Tables;

namespace PathSystem.Database
{
    public class EFContext : DbContext
    {
        public DbSet<MapPosition> Map { get; set; }
        public DbSet<PathPosition> PathPositions { get; set; }
        public DbSet<Path> Paths { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityPosition> EntitiesPosition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=PathSystemDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // map seed
            var mapPoints = new JSON().Deserialize();
            modelBuilder.Entity<MapPosition>().HasData(mapPoints);
        }
    }
}
