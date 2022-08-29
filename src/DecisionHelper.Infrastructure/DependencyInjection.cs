using DecisionHelper.Domain.Abstract;
using DecisionHelper.Infrastructure.Repositories.InMemory;
using DecisionHelper.Infrastructure.Repositories.Redis;
using DecisionHelper.Infrastructure.Seeds;
using Microsoft.Extensions.DependencyInjection;

namespace DecisionHelper.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool useRedis)
        {
            if (useRedis)
            {
                services.AddSingleton<IDecisionTreeRepository, RedisDecisionTreeRepository>();
                services.AddTransient<IDecisionTreeSeed, RedisDecisionTreeSeed>();
            }
            else
            {
                services.AddSingleton<IDecisionTreeRepository, InMemoryDecisionTreeRepository>();
                services.AddTransient<IDecisionTreeSeed, InMemoryDecisionTreeSeed>();
            }

            return services;
        }

        public static void SeedInitialData(this IServiceProvider serviceProvider)
        {
            var treeSeed = serviceProvider.GetRequiredService<IDecisionTreeSeed>();
            treeSeed.SeedAsync().Wait();
        }
    }
}
