using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class ProductInputType: InputObjectGraphType
    {
        public ProductInputType()
        {
            Name = "ProductInput";
            //Field<NonNullGraphType<IdGraphType>>("ProductId");
            Field<NonNullGraphType<StringGraphType>>("ProductName");
            Field<StringGraphType>("ProductDescription");
            Field<NonNullGraphType<DecimalGraphType>>("ProductPrice");
            Field<StringGraphType>("ProductImageUrl");
            Field<NonNullGraphType<BooleanGraphType>>("IsAvalable");
            Field<NonNullGraphType<CategoryEnumType>>("ProductCategory");
        }
    }
}
