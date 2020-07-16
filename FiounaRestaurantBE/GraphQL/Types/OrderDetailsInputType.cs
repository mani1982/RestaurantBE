using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class OrderDetailsInputType: InputObjectGraphType
    {
        public OrderDetailsInputType()
        {
            Name = "OrderDetailsInput";
            //Field<NonNullGraphType<IdGraphType>>("OrderDetailId");
            Field<IntGraphType>("Quantity");
            Field<DecimalGraphType>("Price");
            Field<NonNullGraphType<IdGraphType>>("ProductId");
            Field<NonNullGraphType<IdGraphType>>("OrderId");
        }
    }
}
