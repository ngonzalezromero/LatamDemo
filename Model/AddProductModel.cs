
using System;
using System.Collections.Generic;
using Latam.Domain;

namespace Latam.Model
{
    public sealed class AddProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DataSource { get; set; }
        public List<CategoryModel> Categories { get; set; }

        public OrderModel Order { get; set; }

        public AddProductModel ToModel(Product product)
        {
            Id = product.ProductId;
            Name = product.ProductName;
            return this;
        }
        public Product ToEntitiy()
        {
            var categories = new List<ProductCategory>();

            foreach (var item in Categories)
            {
                categories.Add(new ProductCategory() { CategoryId = item.Id, ProductId = Id });
            }

            return new Product()
            {
                ProductName = Name,
                ProductCategories = categories
            };
        }

    }
}
