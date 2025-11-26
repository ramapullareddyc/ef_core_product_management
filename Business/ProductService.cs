using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EFCore.DataAccess;
using EFCore.Models;

namespace EFCore.Business
{
    public class ProductService
    {
        private readonly ProductDbContext _context;

        public ProductService(ProductDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Set<Product>().ToList();
        }

        public Product GetProduct(int productId)
        {
            return _context.Set<Product>().Find(productId);
        }

        public int CreateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required.");

            if (product.Price < 0)
                throw new ArgumentException("Price cannot be negative.");

            if (product.StockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.");

            _context.Set<Product>().Add(product);
            _context.SaveChanges();
            return product.ProductId;
        }

        public void UpdateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required.");

            if (product.Price < 0)
                throw new ArgumentException("Price cannot be negative.");

            if (product.StockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.");

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = _context.Set<Product>().Find(productId);
            if (product != null)
            {
                _context.Set<Product>().Remove(product);
                _context.SaveChanges();
            }
        }

        public void UpdateProductStock(int productId, int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.");

            var product = _context.Set<Product>().Find(productId);
            if (product == null)
                throw new ArgumentException($"Product with ID {productId} not found.");

            product.StockQuantity = newQuantity;
            _context.SaveChanges();
        }
    }
}
