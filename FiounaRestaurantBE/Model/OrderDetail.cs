using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Model
{
    public class OrderDetail
    {
        public Guid OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid? OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }


    }
}
