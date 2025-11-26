using Microsoft.EntityFrameworkCore;
using EFCore.Models;
using System;

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
                entity.ToTable("Products", "public");
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.ProductId)
                    .HasColumnName("ProductId")
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(500);

                entity.Property(p => p.Price)
                    .HasColumnName("Price")
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.StockQuantity)
                    .HasColumnName("StockQuantity");

                entity.Property(p => p.CategoryId)
                    .HasColumnName("CategoryId");

                entity.Property(p => p.SupplierId)
                    .HasColumnName("SupplierId");

                entity.Property(p => p.SKU)
                    .HasColumnName("SKU");

                entity.Property(p => p.Weight)
                    .HasColumnName("Weight");

                entity.Property(p => p.Dimensions)
                    .HasColumnName("Dimensions");

                entity.Property(p => p.IsDiscontinued)
                    .HasColumnName("IsDiscontinued")
                    .HasConversion<int>();

                entity.Property(p => p.ReorderLevel)
                    .HasColumnName("ReorderLevel");

                entity.Property(p => p.CreatedDate)
                    .HasColumnName("CreatedDate");

                entity.Property(p => p.ModifiedDate)
                    .HasColumnName("ModifiedDate");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
} 