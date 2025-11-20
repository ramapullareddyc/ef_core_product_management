using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using EFCore.Models;
using System;

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

            // Configure table mappings with PostgreSQL schema
            modelBuilder.Entity<Product>().ToTable("products", schema: "productmanagement_dbo");
            modelBuilder.Entity<Category>().ToTable("categories", schema: "productmanagement_dbo");
            modelBuilder.Entity<Supplier>().ToTable("suppliers", schema: "productmanagement_dbo");
            modelBuilder.Entity<ProductHistory>().ToTable("producthistory", schema: "productmanagement_dbo");
            modelBuilder.Entity<ProductStats>().ToTable("productstats", schema: "productmanagement_dbo");

            // Configure column mappings for Product
            modelBuilder.Entity<Product>().Property(p => p.ProductId).HasColumnName("productid");
            modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<Product>().Property(p => p.Description).HasColumnName("description");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnName("price");
            modelBuilder.Entity<Product>().Property(p => p.StockQuantity).HasColumnName("stockquantity");
            modelBuilder.Entity<Product>().Property(p => p.CategoryId).HasColumnName("categoryid");
            modelBuilder.Entity<Product>().Property(p => p.SupplierId).HasColumnName("supplierid");
            modelBuilder.Entity<Product>().Property(p => p.SKU).HasColumnName("sku");
            modelBuilder.Entity<Product>().Property(p => p.Weight).HasColumnName("weight");
            modelBuilder.Entity<Product>().Property(p => p.Dimensions).HasColumnName("dimensions");
            modelBuilder.Entity<Product>().Property(p => p.IsDiscontinued).HasColumnName("isdiscontinued").HasConversion<int>();
            modelBuilder.Entity<Product>().Property(p => p.ReorderLevel).HasColumnName("reorderlevel");
            modelBuilder.Entity<Product>().Property(p => p.CreatedDate).HasColumnName("createddate");
            modelBuilder.Entity<Product>().Property(p => p.ModifiedDate).HasColumnName("modifieddate");

            // Configure column mappings for Category
            modelBuilder.Entity<Category>().Property(c => c.CategoryId).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("name");
            modelBuilder.Entity<Category>().Property(c => c.Description).HasColumnName("description");
            modelBuilder.Entity<Category>().Property(c => c.ParentCategoryId).HasColumnName("parentcategoryid");
            modelBuilder.Entity<Category>().Property(c => c.CreatedDate).HasColumnName("createddate");

            // Configure column mappings for Supplier
            modelBuilder.Entity<Supplier>().Property(s => s.SupplierId).HasColumnName("supplierid");
            modelBuilder.Entity<Supplier>().Property(s => s.Name).HasColumnName("name");
            modelBuilder.Entity<Supplier>().Property(s => s.Address).HasColumnName("address");
            modelBuilder.Entity<Supplier>().Property(s => s.ContactPerson).HasColumnName("contactperson");
            modelBuilder.Entity<Supplier>().Property(s => s.Phone).HasColumnName("phone");
            modelBuilder.Entity<Supplier>().Property(s => s.Email).HasColumnName("email");
            modelBuilder.Entity<Supplier>().Property(s => s.IsActive).HasColumnName("isactive").HasConversion<int>();

            // Configure column mappings for ProductHistory
            modelBuilder.Entity<ProductHistory>().Property(h => h.HistoryId).HasColumnName("historyid");
            modelBuilder.Entity<ProductHistory>().Property(h => h.ProductId).HasColumnName("productid");
            modelBuilder.Entity<ProductHistory>().Property(h => h.Action).HasColumnName("action");
            modelBuilder.Entity<ProductHistory>().Property(h => h.OldPrice).HasColumnName("oldprice");
            modelBuilder.Entity<ProductHistory>().Property(h => h.NewPrice).HasColumnName("newprice");
            modelBuilder.Entity<ProductHistory>().Property(h => h.OldStock).HasColumnName("oldstock");
            modelBuilder.Entity<ProductHistory>().Property(h => h.NewStock).HasColumnName("newstock");
            modelBuilder.Entity<ProductHistory>().Property(h => h.ActionDate).HasColumnName("actiondate");
            modelBuilder.Entity<ProductHistory>().Property(h => h.ModifiedBy).HasColumnName("modifiedby");

            // Configure column mappings for ProductStats
            modelBuilder.Entity<ProductStats>().Property(ps => ps.StatId).HasColumnName("statid");
            modelBuilder.Entity<ProductStats>().Property(ps => ps.TotalProducts).HasColumnName("totalproducts");
            modelBuilder.Entity<ProductStats>().Property(ps => ps.AveragePrice).HasColumnName("averageprice");
            modelBuilder.Entity<ProductStats>().Property(ps => ps.TotalStockValue).HasColumnName("totalstockvalue");
            modelBuilder.Entity<ProductStats>().Property(ps => ps.LowStockCount).HasColumnName("lowstockcount");
            modelBuilder.Entity<ProductStats>().Property(ps => ps.DiscontinuedCount).HasColumnName("discontinuedcount");
            modelBuilder.Entity<ProductStats>().Property(ps => ps.LastUpdated).HasColumnName("lastupdated");

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
