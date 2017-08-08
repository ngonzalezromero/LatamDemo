using System.Collections.Generic;

namespace Latam.Domain
{
    public sealed class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();


    }
}