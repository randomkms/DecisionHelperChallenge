using DecisionHelper.Domain.Abstract;
using DecisionHelper.Domain.Models;
using DecisionHelper.Infrastructure.Persistence;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace DecisionHelper.Infrastructure.Repositories.Redis
{
    internal class RedisDecisionTreeRepository : IDecisionTreeRepository
    {
        private readonly IRedisClient _redisClient;

        public RedisDecisionTreeRepository(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public async Task<DecisionNode?> GetDecisionTreeAsync(string treeName)
        {
            return await _redisClient.GetDb(RedisConsts.TreesDb).GetAsync<DecisionNode>(treeName);
        }

        public async Task<IReadOnlyCollection<string>> GetDecisionTreesAsync()
        {
            var searchKeys = await _redisClient.GetDb(RedisConsts.TreesDb).SearchKeysAsync("*");
            return searchKeys.ToArray();
        }

        public async Task<DecisionNode?> GetDecisionByIdAsync(Guid chosenNodeId)
        {
            return await _redisClient.GetDb(RedisConsts.NodesDb).GetAsync<DecisionNode>(chosenNodeId.ToString("N"));
        }
    }
}
