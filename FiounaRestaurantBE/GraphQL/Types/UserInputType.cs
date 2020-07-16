using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class UserInputType: InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "UserInput";
            Field<IdGraphType>("UserId");
            Field<NonNullGraphType<StringGraphType>>("FirstName");
            Field<NonNullGraphType<StringGraphType>>("LastName");
        }
    }
}
