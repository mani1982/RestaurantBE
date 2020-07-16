using FiounaRestaurantBE.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Repository
{
    public class OrderRepository
    {
        private readonly FiounaRestaurantDbContext _context;

        public OrderRepository(FiounaRestaurantDbContext context)
        {
            _context = context;
        }

        //private readonly List<Order> orders = new List<Order>
        //{
        //    new Order
        //    {
        //        OrderId = Guid.NewGuid(),
                
        //    }
        //};

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<List<Order>> GetOrdersByUserId(Guid userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<Order> CreateOrder(Order order)
        {
            order.OrderId = Guid.NewGuid();
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
