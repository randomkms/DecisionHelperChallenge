using DecisionHelper.Domain.Abstract;
using DecisionHelper.Infrastructure.Persistence;
using DecisionHelper.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DecisionHelper.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var storage = new Storage();
            services.AddSingleton<IStorage>(storage);
            services.AddSingleton<IDecisionTreeRepository, DecisionTreeRepository>();

            new DecisionTreeSeed(storage).Seed();

            return services;
        }
    }
}
