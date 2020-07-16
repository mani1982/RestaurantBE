using FiounaRestaurantBE.Model;
using FiounaRestaurantBE.Repository;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL.Types
{
    public class UserType:ObjectGraphType<User>
    {
        public UserType(AddressRepository addressRepository,OrderRepository orderRepository)
        {
            Field(f => f.UserId, type: typeof(IdGraphType));
            Field(f => f.FirstName);
            Field(f => f.LastName);

            Field<ListGraphType<AddressType>>(
                    "Addresses",
                    resolve: context =>
                    {
                        return addressRepository.GetAddressesByUserIdAsync(context.Source.UserId);
                    }
                );

            Field<ListGraphType<OrderType>>(
                    "CustomerOrders",
                    resolve: context =>
                    {
                        return orderRepository.GetOrdersByUserId(context.Source.UserId);
                    }
                );
        }
    }
}
