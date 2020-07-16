using FiounaRestaurantBE.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Repository
{
    public class ProductRepository
    {
        private readonly FiounaRestaurantDbContext _context;
        public ProductRepository(FiounaRestaurantDbContext context)
        {
            _context = context;
        }

//        return await Task.Run(() => new List<Product>
//            {
//                new Product
//                {
//                    ProductId = Guid.NewGuid(),
//                    ProductName = "KashkeBademjoon",
//                    ProductDescription = "KashkeBademjoon description",
//                    ProductPrice = 10.99m,
//                    ProductCategory = Category.Appetizers

//    },
//                new Product
//                {
//                    ProductId = Guid.NewGuid(),
//                    ProductName = "baghaliPolo",
//                    ProductDescription = "baghaliPolo description",
//                    ProductPrice = 15.99m,
//                    ProductCategory = Category.DinnerBox

//},
//                new Product
//                {
//                    ProductId = Guid.NewGuid(),
//                    ProductName = "Doogh",
//                    ProductDescription = "Doogh description",
//                    ProductPrice = 1.99m,
//                    ProductCategory = Category.Beverages

//                }
//            });

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Category category)
        {
            return await _context.Products.Where(p => p.ProductCategory == category).ToListAsync();
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.ProductId = Guid.NewGuid();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
