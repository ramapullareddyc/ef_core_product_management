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




        // Get product history
        public static async Task<List<ProductHistory>> GetProductHistoryByProductId(ApplicationDbContext context, int productId)
        {
            return await context.ProductHistory
                .Where(h => h.ProductId == productId)
                .OrderByDescending(h => h.ActionDate)
                .ToListAsync();
        }

        // Get products with low stock
        public static async Task<List<Product>> GetLowStockProducts(ApplicationDbContext context, int threshold = 10)
        {
            return await context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.StockQuantity <= threshold)
                .OrderBy(p => p.StockQuantity)
                .ToListAsync();
        }

        // Get products by price range
        public static async Task<List<Product>> GetProductsByPriceRange(ApplicationDbContext context, decimal minPrice, decimal maxPrice)
        {
            return await context.Products
                .Include(p => p.Category)
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .OrderBy(p => p.Price)
                .ToListAsync();
        }

        // Get supplier performance (number of products supplied)
        public static async Task<List<Supplier>> GetSupplierPerformance(ApplicationDbContext context)
        {
            return await context.Suppliers
                .Include(s => s.Products)
                .OrderByDescending(s => s.Products.Count)
                .ToListAsync();
        }

        // Get category statistics (number of products per category)
        public static async Task<List<Category>> GetCategoryStatistics(ApplicationDbContext context)
        {
            return await context.Categories
                .Include(c => c.Products)
                .OrderByDescending(c => c.Products.Count)
                .ToListAsync();
        }
    }
}
