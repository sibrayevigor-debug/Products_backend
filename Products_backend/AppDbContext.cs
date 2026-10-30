using Microsoft.EntityFrameworkCore;
using Products_backend.models;

namespace Products_backend
{
    public class AppDbContext : DbContext
    {
        /// Конструктор для внедрения конфигурации (строки подключения) через Dependency Injection
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Product> Products => Set<Product>();

        // Тонкая настройка базы данных через Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка таблицы Warehouse
            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse");
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name)
                .HasMaxLength(200)
                .IsUnicode()
                .IsRequired();
            });

            // Настройка таблицы Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                .HasMaxLength(200)
                .IsUnicode()
                .IsRequired();

                entity.Property(p => p.Price)
                .HasColumnType("decimal(12,2)").IsRequired();

                entity.Property(p => p.Quantity).IsRequired();
                entity.Property(p => p.Length_mm).IsRequired();
                entity.Property(p => p.Weight_kg).IsRequired();
                entity.Property(p => p.Height_mm).IsRequired();

                entity.Property(p => p.Weight_kg)
                  .HasColumnType("decimal(8,3)").IsRequired();

                entity.HasOne(p => p.Warehouse)
                  .WithMany(w => w.Products)
                  .HasForeignKey(p => p.Warehouse_id)
                  .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
