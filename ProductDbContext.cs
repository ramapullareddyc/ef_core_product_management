using System;
using Microsoft.EntityFrameworkCore;
using EFCore.Models;

namespace EFCore.DataAccess
{
    public class ProductDatabaseContext : DbContext
    {
        static ProductDatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public ProductDatabaseContext(DbContextOptions<ProductDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                // Apply schema mapping for table name
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

                // Configure boolean to int conversion for PostgreSQL compatibility
                entity.Property(p => p.IsDiscontinued).HasConversion<int>();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
