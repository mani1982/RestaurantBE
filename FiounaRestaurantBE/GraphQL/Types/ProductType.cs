using FiounaRestaurantBE.Model;
using GraphQL.Instrumentation;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class ProductType: ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(t => t.ProductId,type: typeof(IdGraphType));
            Field(t => t.ProductName);
            Field(t => t.ProductDescription);
            Field(t => t.ProductPrice);
            Field(t => t.ProductImageUrl);
            //Field(t => t.ProductCategory);
            Field(t => t.IsAvalable);
            Field<CategoryEnumType>("ProductCategory");

        }
    }
}
