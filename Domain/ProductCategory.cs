namespace Latam.Domain
{
    public sealed class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }


    }
}