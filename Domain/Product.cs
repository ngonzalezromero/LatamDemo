using System.Collections.Generic;

namespace Latam.Domain
{
    public sealed class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

    }
}
