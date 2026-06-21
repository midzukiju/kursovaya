using Microsoft.EntityFrameworkCore;

namespace kurs.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<kurs.Models.Order> Orders { get; set; }
        public DbSet<kurs.Models.IndividualOrder> IndividualOrders { get; set; }
        public DbSet<kurs.Models.Catalog> Catalogs { get; set; }
        public DbSet<kurs.Models.Client> Clients { get; set; }
        public DbSet<kurs.Models.Material> Materials { get; set; }
        public DbSet<kurs.Models.Model> Models { get; set; }
        public DbSet<kurs.Models.Supply> Supplies { get; set; }
        public DbSet<kurs.Models.Supplier> Suppliers { get; set; }
        public DbSet<kurs.Models.WorkProcess> WorkProcesses { get; set; }
        public DbSet<kurs.Models.Employee> Employees { get; set; }
        public DbSet<kurs.Models.Urgency> Urgencies { get; set; }
        public DbSet<kurs.Models.ThermalProtection> ThermalProtections { get; set; }
        public DbSet<kurs.Models.Color> Colors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString =
                    "Server=tompsons.beget.tech;" +
                    "Port=3306;" +
                    "Database=tompsons_stud10;" +
                    "Uid=tompsons_stud10;" +
                    "Pwd=8Y9jVNB9cf;" +
                    "Connection Timeout=30;";

                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        // УБРАЛИ OnModelCreating полностью!
        // EF Core сам определит структуру по атрибутам в моделях
    }
}