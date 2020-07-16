using GraphQL.Authorization;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FiounaRestaurantBE.Infrastructure
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddGraphQLAuth(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.AddSingleton(s =>
            {
                var authSettings = new AuthorizationSettings();

                authSettings.AddPolicy(Policies.Admin, _ => _.RequireClaim(ClaimTypes.Role, "admin"));
                authSettings.AddPolicy(Policies.Customer, _ => _.RequireClaim(ClaimTypes.Role, "customer"));

                return authSettings;
            });
        }

        //public static void AddGraphQLAuth(this IServiceCollection services, Action<AuthorizationSettings, IServiceProvider> configure)
        //{
        //    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //    services.TryAddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
        //    services.AddTransient<IValidationRule, AuthorizationValidationRule>();

        //    services.TryAddTransient(s =>
        //    {
        //        var authSettings = new AuthorizationSettings();
        //        configure(authSettings, s);
        //        return authSettings;
        //    });
        //}

        //public static void AddGraphQLAuth(this IServiceCollection services, Action<AuthorizationSettings> configure)
        //{
        //    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //    services.TryAddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
        //    services.AddTransient<IValidationRule, AuthorizationValidationRule>();

        //    services.TryAddTransient(s =>
        //    {
        //        var authSettings = new AuthorizationSettings();
        //        configure(authSettings);
        //        return authSettings;
        //    });
        //}
    }
}
