using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Latam.Domain;
using Latam.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace Latam.Repository
{
    public sealed class EFRepository : IRepository
    {
        private readonly EFContext _context;
        public EFRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                _context.Product.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception e)
            {
                await Console.Error.WriteAsync(e.ToString());
                return null;
            }
        }

        public async Task<Order> AddOrderAsync(Order order, Product product)
        {
            try
            {
                var orderDb = await _context.Order.Include(x => x.Products).FirstAsync(x => x.OrderId == order.OrderId);
                orderDb.Products.Add(product);
                _context.Order.Update(orderDb);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception e)
            {
                await Console.Error.WriteAsync(e.ToString());
                return null;
            }
        }
        public Task<List<Product>> ListAllProductsAsync()
        {
            return _context.Product.Include(x => x.ProductCategories).ThenInclude(x => x.Category).ToListAsync();
        }

        public Task<List<Category>> ListAllCategoriesAsync()
        {
            return _context.Category.ToListAsync();
        }

        public Task<List<Order>> ListAllOrdersAsync()
        {
            return _context.Order.ToListAsync();
        }
    }
}
