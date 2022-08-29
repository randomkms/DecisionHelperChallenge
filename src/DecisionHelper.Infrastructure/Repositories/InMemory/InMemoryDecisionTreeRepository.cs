using DecisionHelper.Domain.Abstract;
using DecisionHelper.Domain.Models;
using DecisionHelper.Infrastructure.Persistence;

namespace DecisionHelper.Infrastructure.Repositories.InMemory
{
    internal class InMemoryDecisionTreeRepository : IDecisionTreeRepository
    {
        private readonly IInMemoryStorage _storage;

        public InMemoryDecisionTreeRepository(IInMemoryStorage storage)
        {
            _storage = storage;
        }

        public Task<IReadOnlyCollection<DecisionTreeInfo>> GetDecisionTreesAsync()
        {
            return Task.FromResult<IReadOnlyCollection<DecisionTreeInfo>>(_storage.TreesInfo.ToArray());
        }

        public Task<DecisionNode?> GetDecisionTreeRootAsync(string treeName)
        {
            if (_storage.Trees.TryGetValue(treeName, out var node))
                return Task.FromResult<DecisionNode?>(node);

            return Task.FromResult<DecisionNode?>(null);
        }

        public Task<DecisionNode?> GetDecisionByIdAsync(Guid chosenNodeId)
        {
            if (_storage.Nodes.TryGetValue(chosenNodeId, out var node))
                return Task.FromResult<DecisionNode?>(node);

            return Task.FromResult<DecisionNode?>(null);
        }
    }
}
