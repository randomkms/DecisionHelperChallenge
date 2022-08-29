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

        public async Task<DecisionNode?> GetDecisionTreeRootAsync(string treeName)
        {
            return await _redisClient.GetDb(RedisConsts.TreesDb).GetAsync<DecisionNode>(treeName);
        }

        public async Task<IReadOnlyCollection<DecisionTreeInfo>> GetDecisionTreesAsync()
        {
            var treeInfos = await _redisClient.GetDb(RedisConsts.TreesInfoDb)
                .GetAsync<IReadOnlyCollection<DecisionTreeInfo>>(RedisConsts.TreesInfoListKey);
            return treeInfos ?? Array.Empty<DecisionTreeInfo>();
        }

        public async Task<DecisionNode?> GetDecisionByIdAsync(Guid chosenNodeId)
        {
            return await _redisClient.GetDb(RedisConsts.NodesDb).GetAsync<DecisionNode>(chosenNodeId.ToString("N"));
        }
    }
}
