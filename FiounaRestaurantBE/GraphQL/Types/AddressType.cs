using FiounaRestaurantBE.Model;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class AddressType: ObjectGraphType<Address>
    {
        public AddressType()
        {
            Field(f => f.AddressId, type: typeof(IdGraphType));
            Field(f => f.Address1);
            Field(f => f.Address2);
            Field(f => f.City);
            Field(f => f.State);
            Field(f => f.ZipCode);
        }
    }
}
