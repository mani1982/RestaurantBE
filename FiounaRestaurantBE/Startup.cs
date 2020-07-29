using FiounaRestaurantBE.GraphQL;
using FiounaRestaurantBE.Model;
using FiounaRestaurantBE.Repository;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FiounaRestaurantBE.Infrastructure;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.IdentityModel.Logging;
using System.Security.Claims;
using IdentityServer4.Services;
using GraphQL.Validation;

namespace FiounaRestaurantBE
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<FiounaRestaurantDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:FiounaRestaurantDb"]), ServiceLifetime.Transient);

            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<FiounaRestaurantDbContext>();

            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                //o.Authority = "https://localhost:8787";
                //o.Audience = "resourceapi";
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddTransient<IProfileService, IdentityClaimsProfileService>();
            services.AddScoped<IUserRepository, UserRepository>();

            //services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<FiounaRestaurantDbContext>().AddDefaultTokenProviders();


            services.AddScoped<ProductRepository>();
            services.AddScoped<AddressRepository>();
            services.AddScoped<OrderDetailsRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<UserRepository>();



            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<KestrelServerOptions>(options =>
                {
                    options.AllowSynchronousIO = true;
                });

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddScoped<ISchema,FiounaRestaurantSchema>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddHttpContextAccessor();

            services.AddGraphQLAuth();

            services.AddGraphQL(g => { g.ExposeExceptions = true; })
    .AddGraphTypes(ServiceLifetime.Scoped)
    .AddUserContextBuilder(context => new GraphQLUserContext { User = context.User })
    //.AddUserContextBuilder(httpContext => httpContext.User)
    .AddDataLoader()
    .AddWebSockets();


            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
            });

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            var validationRules = app.ApplicationServices.GetServices<IValidationRule>();


            app.UseWebSockets();
            app.UseGraphQLWebSockets<FiounaRestaurantSchema>("/graphql");

            app.UseGraphQL<FiounaRestaurantSchema>();
            ////app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseGraphiQLServer(new GraphiQLOptions());


            //app.UseGraphiql("/graphiql", options =>
            //{
            //    options.GraphQlEndpoint = "/graphql";
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            PrepDB.PrepPopulation(app);
        }
    }



    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Migrate(serviceScope.ServiceProvider.GetService<FiounaRestaurantDbContext>());
            }
        }

        public static void Migrate(FiounaRestaurantDbContext dbContext)
        {
            try
            {
                dbContext.Database.Migrate();
            }
            catch(Exception e)
            {
                Console.WriteLine($" sql error: {e.Message}");
                Console.WriteLine($" sql inner exception: {e.InnerException}");

            }
        }
    }
}
