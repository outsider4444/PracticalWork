using Microsoft.EntityFrameworkCore;
using PracticalWork.Models;

namespace PracticalWork.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Computer> Computers { get; set; }
        public DbSet<GraphicsCard> GraphicCards { get; set; }
        public DbSet<HardDrive> HardDrives { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<PowerSupply> PowerSupplies { get; set; }
        public DbSet<Ram> Rams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=PracticalWork;Trusted_Connection=True;TrustServerCertificate=true;");
        }
    }
}
