using FiounaRestaurantBE.Model;
using FiounaRestaurantBE.Repository;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class OrderType: ObjectGraphType<Order>
    {
        public OrderType(OrderDetailsRepository orderDetailsRepository)
        {
            Field(f => f.OrderId, type: typeof(IdGraphType));
            Field(f => f.Total);
            Field(f => f.SubTotal);
            Field(f => f.Tax);
            Field(f => f.UserId, type: typeof(IdGraphType));

            Field<ListGraphType<OrderDetailsType>>(
                "orderdetails",
                resolve: context =>
                {
                    return orderDetailsRepository.GetOrderDetailsByIdAsync(context.Source.OrderId);
                });


        }
    }
}
