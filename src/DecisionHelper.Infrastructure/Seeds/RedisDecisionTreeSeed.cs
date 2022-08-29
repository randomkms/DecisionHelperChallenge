using DecisionHelper.Domain.Models;
using DecisionHelper.Infrastructure.Persistence;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace DecisionHelper.Infrastructure.Seeds
{
    internal class RedisDecisionTreeSeed : IDecisionTreeSeed
    {
        private readonly IRedisClient _redisClient;

        public RedisDecisionTreeSeed(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public async Task SeedAsync()
        {
            await _redisClient.GetDb(RedisConsts.TreesDb).FlushDbAsync();
            await _redisClient.GetDb(RedisConsts.NodesDb).FlushDbAsync();

            foreach (var (name, tree) in DecisionTreesFilesHelper.GetTrees())
            {
                await _redisClient.GetDb(RedisConsts.TreesDb).AddAsync(name, tree);
                await AddNodesToDictAsync(tree);
            }
        }

        private async Task AddNodesToDictAsync(DecisionNode node)
        {
            await _redisClient.GetDb(RedisConsts.NodesDb).AddAsync(node.Id.ToString("N"), node);
            foreach (var child in node.Children)
                await AddNodesToDictAsync(child);
        }
    }
}
