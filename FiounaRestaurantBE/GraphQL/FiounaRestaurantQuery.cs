using FiounaRestaurantBE.GraphQL.Types;
using FiounaRestaurantBE.Model;
using FiounaRestaurantBE.Repository;
using GraphQL.Language.AST;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.GraphQL
{
    public class FiounaRestaurantQuery: ObjectGraphType
    {
        public FiounaRestaurantQuery(AddressRepository addressRepository, ProductRepository productRepository, 
                                     OrderRepository orderRepository, UserRepository userRepository)
        {
            Field<ListGraphType<AddressType>>(
                "AddressesByUserId",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" }),
                resolve: context =>
                {
                    var userId = context.GetArgument<Guid>("userId");
                    return addressRepository.GetAddressesByUserIdAsync(userId);
                }
            );

            Field<ListGraphType<UserType>>(
                "Users",
                resolve: context => userRepository.GetUseersAsync()
            );

            Field<UserType>(
                "UserById",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType < IdGraphType >>{Name= "userId" }),
                resolve: context =>
                {
                    var userId = context.GetArgument<Guid>("userId");
                    return userRepository.GetUserByIdAsync(userId);
                });

            Field<ListGraphType<ProductType>>(
                "Products",
                resolve: context => productRepository.GetProductsAsync()
            );

            Field<ListGraphType<ProductType>>(
                "ProductsByCategory",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CategoryEnumType>> { Name = "category"}),
                resolve: context =>
                {
                    var category = context.GetArgument<Category>("category");
                    return productRepository.GetProductsByCategoryAsync(category);
                }
            );

            Field<ProductType>(
                "ProductById",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "productId" }),
                resolve: context =>
                {
                    var productId = context.GetArgument<Guid>("productId");
                    return productRepository.GetProductById(productId);
                }
            );

            Field<OrderType>(
                "OrderById",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return orderRepository.GetOrderByIdAsync(id);
                }
            );

            Field<ListGraphType<OrderType>>(
                "OrdersByUserId",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId"}),
                resolve: context =>
                {
                    var userId = context.GetArgument<Guid>("userId");
                    return orderRepository.GetOrdersByUserId(userId);
                }
            );
        }
    }
}
