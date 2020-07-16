using FiounaRestaurantBE.Model;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class OrderDetailsType: ObjectGraphType<OrderDetail>
    {
        public OrderDetailsType()
        {
            Field(x => x.OrderDetailId, type: typeof(IdGraphType));
            Field(x => x.Quantity);
            Field(x => x.Price);
            Field(x => x.OrderId, type: typeof(IdGraphType));
            Field(x => x.ProductId, type: typeof(IdGraphType));

        }
    }
}
