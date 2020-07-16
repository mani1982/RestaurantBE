using FiounaRestaurantBE.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Repository
{
    public class OrderDetailsRepository
    {
        private readonly FiounaRestaurantDbContext _context;

        public OrderDetailsRepository(FiounaRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByIdAsync(Guid orderId)
        {
            return await _context.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
        }

        public async Task<OrderDetail> CreateOrderDetails(OrderDetail orderDetail)
        {
            orderDetail.OrderDetailId = Guid.NewGuid();
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return orderDetail;
        }
    }
}
