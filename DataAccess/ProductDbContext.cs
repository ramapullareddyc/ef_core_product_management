using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
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
                // Apply schema mapping: Products -> Products_mod in schema database-1_dbo
                entity.ToTable("Products_mod", "database-1_dbo");
                entity.HasKey(p => p.ProductId);

                // Apply column mappings from schema_mappings.json
                entity.Property(p => p.ProductId)
                    .HasColumnName("ProductId_mod")
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .HasColumnName("Name_mod")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Description)
                    .HasColumnName("Description_mod")
                    .HasMaxLength(500);

                entity.Property(p => p.Price)
                    .HasColumnName("Price_mod")
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.StockQuantity)
                    .HasColumnName("StockQuantity_mod");

                entity.Property(p => p.CategoryId)
                    .HasColumnName("CategoryId_mod");

                entity.Property(p => p.SupplierId)
                    .HasColumnName("SupplierId_mod");

                entity.Property(p => p.SKU)
                    .HasColumnName("SKU_mod");

                entity.Property(p => p.Weight)
                    .HasColumnName("Weight_mod");

                entity.Property(p => p.Dimensions)
                    .HasColumnName("Dimensions_mod");

                entity.Property(p => p.IsDiscontinued)
                    .HasColumnName("IsDiscontinued_mod")
                    .HasConversion<int>();

                entity.Property(p => p.ReorderLevel)
                    .HasColumnName("ReorderLevel_mod");

                entity.Property(p => p.CreatedDate)
                    .HasColumnName("CreatedDate_mod");

                entity.Property(p => p.ModifiedDate)
                    .HasColumnName("ModifiedDate_mod");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
} 