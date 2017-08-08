using System;
using System.Collections.Generic;
using Latam.Domain;

namespace Latam.Model
{
    public sealed class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductModel ToModel(Product product)
        {
            Id = product.ProductId;
            Name = product.ProductName;
            return this;
        }

    }
}
