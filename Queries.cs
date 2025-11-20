using Microsoft.EntityFrameworkCore;
using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Data
{
    public static partial class Queries
    {
        // Get products with their category and supplier information
        public static async Task<List<Product>> GetProductsWithDetailsAsync(ApplicationDbContext context)
        {
            return await context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        // Get category hierarchy
        public static async Task<List<Category>> GetCategoryHierarchyAsync(ApplicationDbContext context)
        {
            return await context.Categories
                .Include(c => c.SubCategories)
                .Where(c => c.ParentCategoryId == null)
                .ToListAsync();
        }

        // Get products by category
        public static async Task<List<Product>> GetProductsByCategoryAsync(ApplicationDbContext context, int categoryId)
        {
            return await context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        // Get recently created products
        public static async Task<List<Product>> GetRecentlyCreatedProductsAsync(ApplicationDbContext context, int count = 10)
        {
            return await context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        // Get product history
        public static async Task<List<ProductHistory>> GetProductHistoryAsync(ApplicationDbContext context, int productId)
        {
            return await context.ProductHistory
                .Where(h => h.ProductId == productId)
                .OrderByDescending(h => h.ActionDate)
                .ToListAsync();
        }

        // Get products by price range
        public static async Task<List<Product>> GetProductsByPriceRangeAsync(ApplicationDbContext context, decimal minPrice, decimal maxPrice)
        {
            return await context.Products
                .Include(p => p.Category)
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .OrderBy(p => p.Price)
                .ToListAsync();
        }

        // Get supplier performance (number of products supplied)
        public static async Task<List<Supplier>> GetSupplierPerformanceAsync(ApplicationDbContext context)
        {
            return await context.Suppliers
                .Include(s => s.Products)
                .OrderByDescending(s => s.Products.Count)
                .ToListAsync();
        }

        // Get category statistics (number of products per category)
        public static async Task<List<Category>> GetCategoryStatisticsAsync(ApplicationDbContext context)
        {
            return await context.Categories
                .Include(c => c.Products)
                .OrderByDescending(c => c.Products.Count)
                .ToListAsync();
        }
    }
}
