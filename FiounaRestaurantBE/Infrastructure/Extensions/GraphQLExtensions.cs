using GraphQL.Types;
using System.Security.Claims;

namespace FiounaRestaurantBE.Infrastructure.Extensions
{
    public static class GraphQLExtensions
    {
        public static string GetUserId(this ResolveFieldContext<object> context)
        {
            return ((GraphQLUserContext)context.UserContext).User.GetClaimValue(ClaimTypes.NameIdentifier);
        }
    }
}
