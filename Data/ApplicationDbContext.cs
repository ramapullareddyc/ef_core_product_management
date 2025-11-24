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

            // Apply schema mappings for PostgreSQL
            modelBuilder.Entity<Product>().ToTable("products", "productmanagement_dbo");
            modelBuilder.Entity<Category>().ToTable("categories", "productmanagement_dbo");
            modelBuilder.Entity<Supplier>().ToTable("suppliers", "productmanagement_dbo");
            modelBuilder.Entity<ProductHistory>().ToTable("producthistory", "productmanagement_dbo");
            modelBuilder.Entity<ProductStats>().ToTable("productstats", "productmanagement_dbo");

            // Apply column mappings for Product
            modelBuilder.Entity<Product>().Property(e => e.ProductId).HasColumnName("productid");
            modelBuilder.Entity<Product>().Property(e => e.Name).HasColumnName("name");
            modelBuilder.Entity<Product>().Property(e => e.Description).HasColumnName("description");
            modelBuilder.Entity<Product>().Property(e => e.Price).HasColumnName("price");
            modelBuilder.Entity<Product>().Property(e => e.StockQuantity).HasColumnName("stockquantity");
            modelBuilder.Entity<Product>().Property(e => e.CategoryId).HasColumnName("categoryid");
            modelBuilder.Entity<Product>().Property(e => e.SupplierId).HasColumnName("supplierid");
            modelBuilder.Entity<Product>().Property(e => e.SKU).HasColumnName("sku");
            modelBuilder.Entity<Product>().Property(e => e.Weight).HasColumnName("weight");
            modelBuilder.Entity<Product>().Property(e => e.Dimensions).HasColumnName("dimensions");
            modelBuilder.Entity<Product>().Property(e => e.IsDiscontinued).HasColumnName("isdiscontinued");
            modelBuilder.Entity<Product>().Property(e => e.ReorderLevel).HasColumnName("reorderlevel");
            modelBuilder.Entity<Product>().Property(e => e.CreatedDate).HasColumnName("createddate");
            modelBuilder.Entity<Product>().Property(e => e.ModifiedDate).HasColumnName("modifieddate");

            // Apply column mappings for Category
            modelBuilder.Entity<Category>().Property(e => e.CategoryId).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(e => e.Name).HasColumnName("name");
            modelBuilder.Entity<Category>().Property(e => e.Description).HasColumnName("description");
            modelBuilder.Entity<Category>().Property(e => e.ParentCategoryId).HasColumnName("parentcategoryid");
            modelBuilder.Entity<Category>().Property(e => e.CreatedDate).HasColumnName("createddate");

            // Apply column mappings for Supplier
            modelBuilder.Entity<Supplier>().Property(e => e.SupplierId).HasColumnName("supplierid");
            modelBuilder.Entity<Supplier>().Property(e => e.Name).HasColumnName("name");
            modelBuilder.Entity<Supplier>().Property(e => e.Address).HasColumnName("address");
            modelBuilder.Entity<Supplier>().Property(e => e.ContactPerson).HasColumnName("contactperson");
            modelBuilder.Entity<Supplier>().Property(e => e.Phone).HasColumnName("phone");
            modelBuilder.Entity<Supplier>().Property(e => e.Email).HasColumnName("email");
            modelBuilder.Entity<Supplier>().Property(e => e.IsActive).HasColumnName("isactive");

            // Apply column mappings for ProductHistory
            modelBuilder.Entity<ProductHistory>().Property(e => e.HistoryId).HasColumnName("historyid");
            modelBuilder.Entity<ProductHistory>().Property(e => e.ProductId).HasColumnName("productid");
            modelBuilder.Entity<ProductHistory>().Property(e => e.Action).HasColumnName("action");
            modelBuilder.Entity<ProductHistory>().Property(e => e.OldPrice).HasColumnName("oldprice");
            modelBuilder.Entity<ProductHistory>().Property(e => e.NewPrice).HasColumnName("newprice");
            modelBuilder.Entity<ProductHistory>().Property(e => e.OldStock).HasColumnName("oldstock");
            modelBuilder.Entity<ProductHistory>().Property(e => e.NewStock).HasColumnName("newstock");
            modelBuilder.Entity<ProductHistory>().Property(e => e.ActionDate).HasColumnName("actiondate");
            modelBuilder.Entity<ProductHistory>().Property(e => e.ModifiedBy).HasColumnName("modifiedby");

            // Apply column mappings for ProductStats
            modelBuilder.Entity<ProductStats>().Property(e => e.StatId).HasColumnName("statid");
            modelBuilder.Entity<ProductStats>().Property(e => e.TotalProducts).HasColumnName("totalproducts");
            modelBuilder.Entity<ProductStats>().Property(e => e.AveragePrice).HasColumnName("averageprice");
            modelBuilder.Entity<ProductStats>().Property(e => e.TotalStockValue).HasColumnName("totalstockvalue");
            modelBuilder.Entity<ProductStats>().Property(e => e.LowStockCount).HasColumnName("lowstockcount");
            modelBuilder.Entity<ProductStats>().Property(e => e.DiscontinuedCount).HasColumnName("discontinuedcount");
            modelBuilder.Entity<ProductStats>().Property(e => e.LastUpdated).HasColumnName("lastupdated");

            // Configure boolean to int conversions for PostgreSQL
            modelBuilder.Entity<Product>().Property(e => e.IsDiscontinued).HasConversion<int>();
            modelBuilder.Entity<Supplier>().Property(e => e.IsActive).HasConversion<int>();

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
