using Microsoft.EntityFrameworkCore;
using EFCore.Models;

namespace EFCore.DataAccess
{
    public partial class ProductDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.ProductId)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Description)
                    .HasMaxLength(500);

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");
            });
        }
    }
}
