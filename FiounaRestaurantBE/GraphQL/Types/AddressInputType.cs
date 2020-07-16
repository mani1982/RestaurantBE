using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class AddressInputType:InputObjectGraphType
    {
        public AddressInputType()
        {
            Name = "AddressInput";
            //Field<IdGraphType>("AddressId");
            Field<NonNullGraphType<StringGraphType>>("Address1");
            Field<StringGraphType>("Address2");
            Field<NonNullGraphType<StringGraphType>>("City");
            Field<NonNullGraphType<StringGraphType>>("State");
            Field<NonNullGraphType<StringGraphType>>("ZipCode");
            Field<NonNullGraphType<IdGraphType>>("UserId");


        }
    }
}
