using BattleArena.Application.Domain;
using BattleArena.Application.Domain.Interfaces;
using BattleArena.Application.Services;
using BattleArena.Application.Services.Interfaces;
using BattleArena.Common;
using BattleArena.Core.PostgreSQL;
using BattleArena.General;
using BattleArena.Mutations;
using BattleArena.Queries;
using BattleArena.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace BattleArena
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment Environment { get; set; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
            Configuration = new ConfigurationBuilder()
                     .AddJsonFile($"AppSettings.{Environment.EnvironmentName}.json")
                     .Build();
            new  AppConfiguration(Configuration, Environment.EnvironmentName);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = AppConfiguration.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            services
                .AddCors()

                .AddDbContext<IDataContext, DataContext>(options =>
                    options.UseNpgsql(connectionString), ServiceLifetime.Scoped)

                .AddScoped<IUserService, UserService>()
                .AddScoped<IBattleService, BattleService>()
                .AddScoped<IUserDomain, UserDomain>()
                .AddScoped<IBattleDomain, BattleDomain>()
                .AddScoped<ITaskDomain, TaskDomain>()

                .AddGraphQLServer()
                    .AddQueryType(t => t.Name("Query"))
                    .AddTypeExtension<UserQueries>()
                    .AddTypeExtension<BattleQueries>()

                    .AddMutationType(t => t.Name("Mutation"))
                    .AddTypeExtension<UserMutations>()
                    .AddTypeExtension<BattleMutations>()

                    .AddType<UserType>()
                    .AddType<BattleType>()

                    .AddFiltering()
                    .AddSorting()
                    .EnableRelaySupport()

                    .AddErrorFilter<ErrorFilter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();

                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/graphql");
                    return Task.CompletedTask;
                });
            });
        }
    }
}
