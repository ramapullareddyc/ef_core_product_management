using System;
using Microsoft.EntityFrameworkCore;
using EFCore.Models;

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

            // Configure Product entity with table and column mappings
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products", "public");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.Property(e => e.SKU).HasColumnName("sku");
                entity.Property(e => e.Weight).HasColumnName("weight");
                entity.Property(e => e.Dimensions).HasColumnName("dimensions");
                entity.Property(e => e.IsDiscontinued).HasColumnName("is_discontinued").HasConversion<int>();
                entity.Property(e => e.ReorderLevel).HasColumnName("reorder_level");
                entity.Property(e => e.CreatedDate).HasColumnName("created_date");
                entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");

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

            // Configure Category entity with table and column mappings
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories", "public");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");
                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                // Configure Category self-referencing relationship
                entity.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Supplier entity with table and column mappings
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers", "public");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.ContactPerson).HasColumnName("contact_person");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasConversion<int>();
            });

            // Configure ProductHistory entity with table and column mappings
            modelBuilder.Entity<ProductHistory>(entity =>
            {
                entity.ToTable("product_history", "public");
                entity.Property(e => e.HistoryId).HasColumnName("history_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Action).HasColumnName("action");
                entity.Property(e => e.OldPrice).HasColumnName("old_price");
                entity.Property(e => e.NewPrice).HasColumnName("new_price");
                entity.Property(e => e.OldStock).HasColumnName("old_stock");
                entity.Property(e => e.NewStock).HasColumnName("new_stock");
                entity.Property(e => e.ActionDate).HasColumnName("action_date");
                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                // Configure ProductHistory relationship
                entity.HasOne(h => h.Product)
                    .WithMany(p => p.History)
                    .HasForeignKey(h => h.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure ProductStats entity with table and column mappings
            modelBuilder.Entity<ProductStats>(entity =>
            {
                entity.ToTable("product_stats", "public");
                entity.Property(e => e.StatId).HasColumnName("stat_id");
                entity.Property(e => e.TotalProducts).HasColumnName("total_products");
                entity.Property(e => e.AveragePrice).HasColumnName("average_price");
                entity.Property(e => e.TotalStockValue).HasColumnName("total_stock_value");
                entity.Property(e => e.LowStockCount).HasColumnName("low_stock_count");
                entity.Property(e => e.DiscontinuedCount).HasColumnName("discontinued_count");
                entity.Property(e => e.LastUpdated).HasColumnName("last_updated");
            });
        }
    }
} 