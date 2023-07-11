using BattleShipsApi.Models;
using BattleShipsApi.Services;

namespace BattleShipsApi.Startup
{
    public static partial class ServiceInitializer
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterSwagger(services);
            RegisterCustomDependencies(services, configuration);
            return services;
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void RegisterCustomDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddScoped<IFleetManager, FleetManager>();
            services.AddScoped<IGameplayManager, GameplayManager>();
            services.AddSingleton<Board>();
        }
    }
}
