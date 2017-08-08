using System;
using System.Collections.Generic;
using Latam.Domain;

namespace Latam.Model
{
    public sealed class OrderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OrderModel ToModel(Order order)
        {
            Id = order.OrderId;
            Name = order.OrderName;
            return this;
        }
        public Order ToEntitiy()
        {
            return new Order()
            {
                OrderName = Name,
                OrderId = Id
            };
        }

    }
}