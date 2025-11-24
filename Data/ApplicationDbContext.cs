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

            // Configure table and schema mappings with column mappings
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products", "productmanagement_dbo");
                entity.Property(p => p.ProductId).HasColumnName("productid");
                entity.Property(p => p.Name).HasColumnName("name");
                entity.Property(p => p.Description).HasColumnName("description");
                entity.Property(p => p.Price).HasColumnName("price");
                entity.Property(p => p.StockQuantity).HasColumnName("stockquantity");
                entity.Property(p => p.CategoryId).HasColumnName("categoryid");
                entity.Property(p => p.SupplierId).HasColumnName("supplierid");
                entity.Property(p => p.SKU).HasColumnName("sku");
                entity.Property(p => p.Weight).HasColumnName("weight");
                entity.Property(p => p.Dimensions).HasColumnName("dimensions");
                entity.Property(p => p.IsDiscontinued).HasColumnName("isdiscontinued").HasConversion<int>();
                entity.Property(p => p.ReorderLevel).HasColumnName("reorderlevel");
                entity.Property(p => p.CreatedDate).HasColumnName("createddate");
                entity.Property(p => p.ModifiedDate).HasColumnName("modifieddate");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories", "productmanagement_dbo");
                entity.Property(c => c.CategoryId).HasColumnName("categoryid");
                entity.Property(c => c.Name).HasColumnName("name");
                entity.Property(c => c.Description).HasColumnName("description");
                entity.Property(c => c.ParentCategoryId).HasColumnName("parentcategoryid");
                entity.Property(c => c.CreatedDate).HasColumnName("createddate");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers", "productmanagement_dbo");
                entity.Property(s => s.SupplierId).HasColumnName("supplierid");
                entity.Property(s => s.Name).HasColumnName("name");
                entity.Property(s => s.Address).HasColumnName("address");
                entity.Property(s => s.ContactPerson).HasColumnName("contactperson");
                entity.Property(s => s.Phone).HasColumnName("phone");
                entity.Property(s => s.Email).HasColumnName("email");
                entity.Property(s => s.IsActive).HasColumnName("isactive").HasConversion<int>();
            });

            modelBuilder.Entity<ProductHistory>(entity =>
            {
                entity.ToTable("producthistory", "productmanagement_dbo");
                entity.Property(h => h.HistoryId).HasColumnName("historyid");
                entity.Property(h => h.ProductId).HasColumnName("productid");
                entity.Property(h => h.Action).HasColumnName("action");
                entity.Property(h => h.OldPrice).HasColumnName("oldprice");
                entity.Property(h => h.NewPrice).HasColumnName("newprice");
                entity.Property(h => h.OldStock).HasColumnName("oldstock");
                entity.Property(h => h.NewStock).HasColumnName("newstock");
                entity.Property(h => h.ActionDate).HasColumnName("actiondate");
                entity.Property(h => h.ModifiedBy).HasColumnName("modifiedby");
            });

            modelBuilder.Entity<ProductStats>(entity =>
            {
                entity.ToTable("productstats", "productmanagement_dbo");
                entity.Property(s => s.StatId).HasColumnName("statid");
                entity.Property(s => s.TotalProducts).HasColumnName("totalproducts");
                entity.Property(s => s.AveragePrice).HasColumnName("averageprice");
                entity.Property(s => s.TotalStockValue).HasColumnName("totalstockvalue");
                entity.Property(s => s.LowStockCount).HasColumnName("lowstockcount");
                entity.Property(s => s.DiscontinuedCount).HasColumnName("discontinuedcount");
                entity.Property(s => s.LastUpdated).HasColumnName("lastupdated");
            });

            // Configure Category self-referencing relationship
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Product relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure ProductHistory relationship
            modelBuilder.Entity<ProductHistory>()
                .HasOne(h => h.Product)
                .WithMany(p => p.History)
                .HasForeignKey(h => h.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductStats is now a standalone table, no relationship configuration needed
        }
    }
} 