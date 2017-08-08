using Latam.Domain;
using Microsoft.EntityFrameworkCore;

namespace Latam.Repository
{
    public class EFContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderName).IsRequired().HasMaxLength(100);
                entity.HasIndex(x => x.OrderName).HasName($"{nameof(Order)}_index");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
                entity.HasIndex(x => x.ProductName).HasName($"{nameof(Product)}_index");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(100);
                entity.HasIndex(x => x.CategoryName).HasName($"{nameof(Category)}_index");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
                entity.HasIndex(x => x.CustomerName).HasName($"{nameof(Customer)}_index");
            });
        }

    }
}
