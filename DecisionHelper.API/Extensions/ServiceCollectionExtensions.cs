using DecisionHelper.API.Abstract;
using DecisionHelper.API.Services;
using DecisionHelper.Domain.Abstract;
using DecisionHelper.Infrastructure.Repositories;

namespace DecisionHelper.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services, string name)
        {
            services.AddCors(c => c.AddPolicy(name,
                options => options.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()));

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<IDecisionTreeRepository, DecisionTreeRepository>();
            services.AddSingleton<IDecisionTreeService, DecisionTreeService>();

            return services;
        }
    }
}
