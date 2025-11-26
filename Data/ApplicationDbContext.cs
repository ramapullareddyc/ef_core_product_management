using System;
using Microsoft.EntityFrameworkCore;
using EFCore.Models;

namespace EFCore.Data
{
    public partial class ApplicationDbContext : DbContext
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

            // Apply schema mappings for Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products", "productmanagement_dbo");
                entity.Property(e => e.ProductId).HasColumnName("productid");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.StockQuantity).HasColumnName("stockquantity");
                entity.Property(e => e.CategoryId).HasColumnName("categoryid");
                entity.Property(e => e.SupplierId).HasColumnName("supplierid");
                entity.Property(e => e.SKU).HasColumnName("sku");
                entity.Property(e => e.Weight).HasColumnName("weight");
                entity.Property(e => e.Dimensions).HasColumnName("dimensions");
                entity.Property(e => e.IsDiscontinued).HasColumnName("isdiscontinued").HasConversion<int>();
                entity.Property(e => e.ReorderLevel).HasColumnName("reorderlevel");
                entity.Property(e => e.CreatedDate).HasColumnName("createddate");
                entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");

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

            // Apply schema mappings for Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories", "productmanagement_dbo");
                entity.Property(e => e.CategoryId).HasColumnName("categoryid");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.ParentCategoryId).HasColumnName("parentcategoryid");
                entity.Property(e => e.CreatedDate).HasColumnName("createddate");

                // Configure Category self-referencing relationship
                entity.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Apply schema mappings for Supplier entity
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers", "productmanagement_dbo");
                entity.Property(e => e.SupplierId).HasColumnName("supplierid");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.ContactPerson).HasColumnName("contactperson");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.IsActive).HasColumnName("isactive").HasConversion<int>();
            });

            // Apply schema mappings for ProductHistory entity
            modelBuilder.Entity<ProductHistory>(entity =>
            {
                entity.ToTable("producthistory", "productmanagement_dbo");
                entity.Property(e => e.HistoryId).HasColumnName("historyid");
                entity.Property(e => e.ProductId).HasColumnName("productid");
                entity.Property(e => e.Action).HasColumnName("action");
                entity.Property(e => e.OldPrice).HasColumnName("oldprice");
                entity.Property(e => e.NewPrice).HasColumnName("newprice");
                entity.Property(e => e.OldStock).HasColumnName("oldstock");
                entity.Property(e => e.NewStock).HasColumnName("newstock");
                entity.Property(e => e.ActionDate).HasColumnName("actiondate");
                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedby");

                // Configure ProductHistory relationship
                entity.HasOne(h => h.Product)
                    .WithMany(p => p.History)
                    .HasForeignKey(h => h.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Apply schema mappings for ProductStats entity
            modelBuilder.Entity<ProductStats>(entity =>
            {
                entity.ToTable("productstats", "productmanagement_dbo");
                entity.Property(e => e.StatId).HasColumnName("statid");
                entity.Property(e => e.TotalProducts).HasColumnName("totalproducts");
                entity.Property(e => e.AveragePrice).HasColumnName("averageprice");
                entity.Property(e => e.TotalStockValue).HasColumnName("totalstockvalue");
                entity.Property(e => e.LowStockCount).HasColumnName("lowstockcount");
                entity.Property(e => e.DiscontinuedCount).HasColumnName("discontinuedcount");
                entity.Property(e => e.LastUpdated).HasColumnName("lastupdated");
            });
        }
    }
}
