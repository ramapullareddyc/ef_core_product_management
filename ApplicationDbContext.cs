using Microsoft.EntityFrameworkCore;
using EFCore.Models;

namespace EFCore.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
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

            // Configure boolean to integer conversions for PostgreSQL
            modelBuilder.Entity<Product>().Property(p => p.IsDiscontinued).HasConversion<int>();
            modelBuilder.Entity<Supplier>().Property(s => s.IsActive).HasConversion<int>();
        }
    }
}
