using System.Collections.Generic;

namespace Latam.Domain
{
    public sealed class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<Order> Orders { get; set; }

    }
}