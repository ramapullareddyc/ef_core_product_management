using System;
using Microsoft.EntityFrameworkCore;
using EFCore.Models;

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
                entity.ToTable("products", "productmanagement_dbo");
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.ProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("productid");

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(p => p.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("price");

                entity.Property(p => p.StockQuantity)
                    .HasColumnName("stockquantity");

                entity.Property(p => p.CategoryId)
                    .HasColumnName("categoryid");

                entity.Property(p => p.SupplierId)
                    .HasColumnName("supplierid");

                entity.Property(p => p.SKU)
                    .HasColumnName("sku");

                entity.Property(p => p.Weight)
                    .HasColumnName("weight");

                entity.Property(p => p.Dimensions)
                    .HasColumnName("dimensions");

                entity.Property(p => p.IsDiscontinued)
                    .HasColumnName("isdiscontinued")
                    .HasConversion<int>();

                entity.Property(p => p.ReorderLevel)
                    .HasColumnName("reorderlevel");

                entity.Property(p => p.CreatedDate)
                    .HasColumnName("createddate");

                entity.Property(p => p.ModifiedDate)
                    .HasColumnName("modifieddate");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
} 
