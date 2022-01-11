using Microsoft.EntityFrameworkCore;
using PathSystem.Models;

namespace PathSystem.Database
{
    public class EFContext : DbContext
    {
        public DbSet<MapPositionModel> Map { get; set; }
        public DbSet<PathPositionModel> PathPositions { get; set; }
        public DbSet<PathModel> Paths { get; set; }
        public DbSet<EntityModel> Entities { get; set; }
        public DbSet<EntityPositionModel> EntitiesPosition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=PathSystemDB;Trusted_Connection=True;");
        }
    }
}
