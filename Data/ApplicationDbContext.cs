using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
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

            // Configure Product entity with schema mappings
            modelBuilder.Entity<Product>(entity =>
            {
                // Apply table mapping: Products -> Products_mod in schema database-1_dbo
                entity.ToTable("Products_mod", "database-1_dbo");

                // Apply column mappings from schema_mappings.json
                entity.Property(p => p.ProductId).HasColumnName("ProductId_mod");
                entity.Property(p => p.Name).HasColumnName("Name_mod");
                entity.Property(p => p.Description).HasColumnName("Description_mod");
                entity.Property(p => p.Price).HasColumnName("Price_mod");
                entity.Property(p => p.StockQuantity).HasColumnName("StockQuantity_mod");
                entity.Property(p => p.CategoryId).HasColumnName("CategoryId_mod");
                entity.Property(p => p.SupplierId).HasColumnName("SupplierId_mod");
                entity.Property(p => p.SKU).HasColumnName("SKU_mod");
                entity.Property(p => p.Weight).HasColumnName("Weight_mod");
                entity.Property(p => p.Dimensions).HasColumnName("Dimensions_mod");
                entity.Property(p => p.IsDiscontinued).HasColumnName("IsDiscontinued_mod").HasConversion<int>();
                entity.Property(p => p.ReorderLevel).HasColumnName("ReorderLevel_mod");
                entity.Property(p => p.CreatedDate).HasColumnName("CreatedDate_mod");
                entity.Property(p => p.ModifiedDate).HasColumnName("ModifiedDate_mod");

                // Preserve existing relationships
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Supplier)
                    .WithMany(s => s.Products)
                    .HasForeignKey(p => p.SupplierId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Category entity with schema mappings
            modelBuilder.Entity<Category>(entity =>
            {
                // Apply table mapping: Categories -> Categories_mod in schema database-1_dbo
                entity.ToTable("Categories_mod", "database-1_dbo");

                // Apply column mappings from schema_mappings.json
                entity.Property(c => c.CategoryId).HasColumnName("CategoryId_mod");
                entity.Property(c => c.Name).HasColumnName("Name_mod");
                entity.Property(c => c.Description).HasColumnName("Description_mod");
                entity.Property(c => c.ParentCategoryId).HasColumnName("ParentCategoryId_mod");
                entity.Property(c => c.CreatedDate).HasColumnName("CreatedDate_mod");

                // Preserve existing self-referencing relationship
                entity.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Supplier entity with schema mappings
            modelBuilder.Entity<Supplier>(entity =>
            {
                // Apply table mapping: Suppliers -> Suppliers_mod in schema database-1_dbo
                entity.ToTable("Suppliers_mod", "database-1_dbo");

                // Apply column mappings from schema_mappings.json
                entity.Property(s => s.SupplierId).HasColumnName("SupplierId_mod");
                entity.Property(s => s.Name).HasColumnName("Name_mod");
                entity.Property(s => s.Address).HasColumnName("Address_mod");
                entity.Property(s => s.ContactPerson).HasColumnName("ContactPerson_mod");
                entity.Property(s => s.Phone).HasColumnName("Phone_mod");
                entity.Property(s => s.Email).HasColumnName("Email_mod");
                entity.Property(s => s.IsActive).HasColumnName("IsActive_mod").HasConversion<int>();
            });

            // Configure ProductHistory entity with schema mappings
            modelBuilder.Entity<ProductHistory>(entity =>
            {
                // Apply table mapping: ProductHistory -> ProductHistory_mod in schema database-1_dbo
                entity.ToTable("ProductHistory_mod", "database-1_dbo");

                // Apply column mappings from schema_mappings.json
                entity.Property(h => h.HistoryId).HasColumnName("HistoryId_mod");
                entity.Property(h => h.ProductId).HasColumnName("ProductId_mod");
                entity.Property(h => h.Action).HasColumnName("Action_mod");
                entity.Property(h => h.OldPrice).HasColumnName("OldPrice_mod");
                entity.Property(h => h.NewPrice).HasColumnName("NewPrice_mod");
                entity.Property(h => h.OldStock).HasColumnName("OldStock_mod");
                entity.Property(h => h.NewStock).HasColumnName("NewStock_mod");
                entity.Property(h => h.ActionDate).HasColumnName("ActionDate_mod");
                entity.Property(h => h.ModifiedBy).HasColumnName("ModifiedBy_mod");

                // Preserve existing relationship
                entity.HasOne(h => h.Product)
                    .WithMany(p => p.History)
                    .HasForeignKey(h => h.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure ProductStats entity with schema mappings
            modelBuilder.Entity<ProductStats>(entity =>
            {
                // Apply table mapping: ProductStats -> ProductStats_mod in schema database-1_dbo
                entity.ToTable("ProductStats_mod", "database-1_dbo");

                // Apply column mappings from schema_mappings.json
                entity.Property(s => s.StatId).HasColumnName("StatId_mod");
                entity.Property(s => s.TotalProducts).HasColumnName("TotalProducts_mod");
                entity.Property(s => s.AveragePrice).HasColumnName("AveragePrice_mod");
                entity.Property(s => s.TotalStockValue).HasColumnName("TotalStockValue_mod");
                entity.Property(s => s.LowStockCount).HasColumnName("LowStockCount_mod");
                entity.Property(s => s.DiscontinuedCount).HasColumnName("DiscontinuedCount_mod");
                entity.Property(s => s.LastUpdated).HasColumnName("LastUpdated_mod");
            });
        }
    }
} 