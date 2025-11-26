using System;
using Microsoft.EntityFrameworkCore;
using EFCore.Models;

namespace EFCore.DataAccess
{
    public partial class ProductDbContext : DbContext
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
                // Apply table and schema mapping
                entity.ToTable("products", "public");
                entity.HasKey(p => p.ProductId);

                // Apply column name mappings
                entity.Property(p => p.ProductId)
                    .HasColumnName("product_id")
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(p => p.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.StockQuantity).HasColumnName("stock_quantity");
                entity.Property(p => p.CategoryId).HasColumnName("category_id");
                entity.Property(p => p.SupplierId).HasColumnName("supplier_id");
                entity.Property(p => p.SKU).HasColumnName("sku");
                entity.Property(p => p.Weight).HasColumnName("weight");
                entity.Property(p => p.Dimensions).HasColumnName("dimensions");
                entity.Property(p => p.IsDiscontinued).HasColumnName("is_discontinued").HasConversion<int>();
                entity.Property(p => p.ReorderLevel).HasColumnName("reorder_level");
                entity.Property(p => p.CreatedDate).HasColumnName("created_date");
                entity.Property(p => p.ModifiedDate).HasColumnName("modified_date");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
