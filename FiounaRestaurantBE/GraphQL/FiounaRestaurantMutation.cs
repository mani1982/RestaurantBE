using FiounaRestaurantBE.GraphQL.Types;
using FiounaRestaurantBE.Model;
using FiounaRestaurantBE.Repository;
using GraphQL.Types;
using GraphQL.Authorization;
using FiounaRestaurantBE.Infrastructure;

namespace FiounaRestaurantBE.GraphQL
{
    public class FiounaRestaurantMutation: ObjectGraphType
    {
        public FiounaRestaurantMutation(AddressRepository addressRepository, UserRepository userRepository, 
            OrderDetailsRepository orderDetailsRepository, OrderRepository orderRepository, ProductRepository productRepository)
        {
            FieldAsync<AddressType>(
                "CreateAddress",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<AddressInputType>> { Name = "address"}),
                resolve: async context =>
                {
                    var newAddress = context.GetArgument<Address>("address");
                    return await context.TryAsyncResolve(async c => await addressRepository.AddAddressAsync(newAddress));
                }).AuthorizeWith(Policies.Customer);

            FieldAsync<UserType>(
                "CreateUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }),
                resolve: async context =>
                {
                    var newUser = context.GetArgument<User>("user");
                    return await context.TryAsyncResolve(async c => await userRepository.AddUser(newUser));
                });

            FieldAsync<ProductType>(
                "CreateProduct",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProductInputType>> { Name = "product" }),
                resolve: async context =>
                {
                    var newProduct = context.GetArgument<Product>("product");
                    return await context.TryAsyncResolve(async c => await productRepository.CreateProductAsync(newProduct));
                }).AuthorizeWith(Policies.Admin);


            FieldAsync<OrderDetailsType>(
                "CreateOrderDetail",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OrderDetailsInputType>> { Name = "orderDetail" }),
                resolve: async context =>
                {
                    var newOrderDetail = context.GetArgument<OrderDetail>("orderDetail");
                    return await context.TryAsyncResolve(async c => await orderDetailsRepository.CreateOrderDetails(newOrderDetail));
                });

            FieldAsync<OrderType>(
                "CreateOrder",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OrderInputType>> { Name = "order" }),
                resolve: async context =>
                {
                    var newOrder = context.GetArgument<Order>("order");
                    return await context.TryAsyncResolve(async c => await orderRepository.CreateOrder(newOrder));
                });
        }
    }
}
