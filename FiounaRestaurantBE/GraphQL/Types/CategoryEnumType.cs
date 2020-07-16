using FiounaRestaurantBE.Model;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class CategoryEnumType: EnumerationGraphType<Category>
    {
    }
}
