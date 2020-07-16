using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Model
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public bool IsAvalable { get; set; }
        public Category ProductCategory { get; set; }

        public OrderDetail OrderDetail { get; set; }

    }
}
