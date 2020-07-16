using GraphQL.Authorization;
using System.Collections.Generic;
using System.Security.Claims;

namespace FiounaRestaurantBE.Infrastructure
{
    public class GraphQLUserContext: Dictionary<string, object>, IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}
