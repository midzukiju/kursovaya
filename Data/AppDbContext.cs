using kurs.Models;  // ← ДОБАВЬ ЭТУ СТРОКУ!
using Microsoft.EntityFrameworkCore;

namespace kurs.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<IndividualOrder> IndividualOrders { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<WorkProcess> WorkProcesses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Urgency> Urgencies { get; set; }
        public DbSet<ThermalProtection> ThermalProtections { get; set; }
        public DbSet<Color> Colors { get; set; }

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
    }
}