using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class OrderInputType: InputObjectGraphType
    {
        public OrderInputType()
        {
            Name = "OrderInputType";
            //Field<IdGraphType>("OrderId");
            Field<DecimalGraphType>("Total");
            Field<DecimalGraphType>("Tax");
            Field<DecimalGraphType>("SubTotal");
            Field<NonNullGraphType<IdGraphType>>("UserId");

        }
    }
}
