using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Latam.Domain;
using Latam.Infraestructure;
using Newtonsoft.Json;

namespace Latam.Repository
{
    public sealed class MemoryRepository : IRepository
    {
        private static ConcurrentDictionary<int, string> _memoryDataBase = new ConcurrentDictionary<int, string>();
        private static Task<List<Category>> _memoryCategories = Task.FromResult(new List<Category>() { new Category() { CategoryId = 1, CategoryName = "new" }, new Category() { CategoryId = 2, CategoryName = "old" } });
        private static Task<List<Order>> _memoryOrders = Task.FromResult(new List<Order>() { new Order() { OrderId = 1, OrderName = "new" } });
        private static int _idSeeder = 1;

        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {

                var serialize = JsonConvert.SerializeObject(product);
                var resut = _memoryDataBase.TryAdd(_idSeeder, serialize);
                if (resut)
                {
                    Interlocked.Increment(ref _idSeeder);
                    product.ProductId = _idSeeder;
                    return product;
                }
                return null;
            }
            catch (Exception e)
            {
                await Console.Error.WriteAsync(e.ToString());
                return null;
            }
        }
        public Task<List<Product>> ListAllProductsAsync()
        {
            var list = new List<Product>();
            foreach (var item in _memoryDataBase)
            {
                var product = JsonConvert.DeserializeObject<Product>(item.Value);
                product.ProductId = item.Key;
                list.Add(product);
            }
            return Task.FromResult(list);
        }

        public Task<List<Category>> ListAllCategoriesAsync()
        {
            return _memoryCategories;
        }

        public Task<List<Order>> ListAllOrdersAsync()
        {
            return _memoryOrders;
        }

        public async Task<Order> AddOrderAsync(Order order, Product product)
        {
            try
            {
                var orderDb = _memoryOrders.Result.First(x => x.OrderId == order.OrderId);
                orderDb.Products.Add(product);
                return orderDb;
            }
            catch (Exception e)
            {
                await Console.Error.WriteAsync(e.ToString());
                return null;
            }
        }
    }
}
