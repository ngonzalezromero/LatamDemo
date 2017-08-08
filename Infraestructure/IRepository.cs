using System.Collections.Generic;
using System.Threading.Tasks;
using Latam.Domain;

namespace Latam.Infraestructure
{
    public interface IRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<List<Product>> ListAllProductsAsync();
        Task<List<Category>> ListAllCategoriesAsync();
        Task<List<Order>> ListAllOrdersAsync();
        Task<Order> AddOrderAsync(Order order, Product product);
    }
}

