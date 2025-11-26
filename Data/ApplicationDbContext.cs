using Microsoft.EntityFrameworkCore;
using EFCore.Models;
using System;

namespace EFCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        static ApplicationDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductHistory> ProductHistory { get; set; }
        public DbSet<ProductStats> ProductStats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table and column mappings for Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products", "public");
                entity.Property(e => e.ProductId).HasColumnName("ProductId");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Description).HasColumnName("Description");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.StockQuantity).HasColumnName("StockQuantity");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
                entity.Property(e => e.SupplierId).HasColumnName("SupplierId");
                entity.Property(e => e.SKU).HasColumnName("SKU");
                entity.Property(e => e.Weight).HasColumnName("Weight");
                entity.Property(e => e.Dimensions).HasColumnName("Dimensions");
                entity.Property(e => e.IsDiscontinued).HasColumnName("IsDiscontinued").HasConversion<int>();
                entity.Property(e => e.ReorderLevel).HasColumnName("ReorderLevel");
                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate");
                entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate");

                // Configure Product relationships
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Supplier)
                    .WithMany(s => s.Products)
                    .HasForeignKey(p => p.SupplierId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure table and column mappings for Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories", "public");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Description).HasColumnName("Description");
                entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryId");
                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate");

                // Configure Category self-referencing relationship
                entity.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure table and column mappings for Supplier entity
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Suppliers", "public");
                entity.Property(e => e.SupplierId).HasColumnName("SupplierId");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Address).HasColumnName("Address");
                entity.Property(e => e.ContactPerson).HasColumnName("ContactPerson");
                entity.Property(e => e.Phone).HasColumnName("Phone");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.IsActive).HasColumnName("IsActive").HasConversion<int>();
            });

            // Configure table and column mappings for ProductHistory entity
            modelBuilder.Entity<ProductHistory>(entity =>
            {
                entity.ToTable("ProductHistory", "public");
                entity.Property(e => e.HistoryId).HasColumnName("HistoryId");
                entity.Property(e => e.ProductId).HasColumnName("ProductId");
                entity.Property(e => e.Action).HasColumnName("Action");
                entity.Property(e => e.OldPrice).HasColumnName("OldPrice");
                entity.Property(e => e.NewPrice).HasColumnName("NewPrice");
                entity.Property(e => e.OldStock).HasColumnName("OldStock");
                entity.Property(e => e.NewStock).HasColumnName("NewStock");
                entity.Property(e => e.ActionDate).HasColumnName("ActionDate");
                entity.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");

                // Configure ProductHistory relationship
                entity.HasOne(h => h.Product)
                    .WithMany(p => p.History)
                    .HasForeignKey(h => h.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure table and column mappings for ProductStats entity
            modelBuilder.Entity<ProductStats>(entity =>
            {
                entity.ToTable("ProductStats", "public");
                entity.Property(e => e.StatId).HasColumnName("StatId");
                entity.Property(e => e.TotalProducts).HasColumnName("TotalProducts");
                entity.Property(e => e.AveragePrice).HasColumnName("AveragePrice");
                entity.Property(e => e.TotalStockValue).HasColumnName("TotalStockValue");
                entity.Property(e => e.LowStockCount).HasColumnName("LowStockCount");
                entity.Property(e => e.DiscontinuedCount).HasColumnName("DiscontinuedCount");
                entity.Property(e => e.LastUpdated).HasColumnName("LastUpdated");
            });
        }
    }
} 