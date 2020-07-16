using System;
using System.Collections.Generic;

namespace FiounaRestaurantBE.Model
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Guid? UserId { get; set; }
        public User Customer { get; set; }

    }
}
