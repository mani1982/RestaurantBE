using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL
{
    public class FiounaRestaurantSchema: Schema
    {
        public FiounaRestaurantSchema(IDependencyResolver resolver):base(resolver)
        {
            Query = resolver.Resolve<FiounaRestaurantQuery>();
            Mutation = resolver.Resolve<FiounaRestaurantMutation>();
        }
    }
}
