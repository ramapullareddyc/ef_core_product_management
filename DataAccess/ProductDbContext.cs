using Microsoft.EntityFrameworkCore;
using EFCore.Models;
using Npgsql;

namespace EFCore.DataAccess
{
    public class ProductDbContext : DbContext
    {
        static ProductDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                // Apply schema mapping for PostgreSQL
                entity.ToTable("products", "productmanagement_dbo");
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.ProductId)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Description)
                    .HasMaxLength(500);

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");

                // Boolean property conversion for PostgreSQL
                entity.Property(p => p.IsDiscontinued).HasConversion<int>();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
} 