using System;
using System.Collections.Generic;
using Latam.Domain;

namespace Latam.Model
{
    public sealed class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryModel ToModel(Category category)
        {
            Id = category.CategoryId;
            Name = category.CategoryName;
            return this;
        }
    }
}
