using DecisionHelper.Domain.Abstract;
using DecisionHelper.Domain.Models;
using DecisionHelper.Infrastructure.Persistence;

namespace DecisionHelper.Infrastructure.Repositories
{
    internal class DecisionTreeRepository : IDecisionTreeRepository
    {
        private readonly IStorage _storage;

        public DecisionTreeRepository(IStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyCollection<string> GetDecisionTrees()
        {
            return _storage.Trees.Keys.ToArray();
        }

        public DecisionNode? GetDecisionTree(string treeName)
        {
            if (_storage.Trees.TryGetValue(treeName, out var node))
                return node;

            return null;
        }

        public DecisionNode? GetDecisionById(Guid chosenNodeId)
        {
            if (_storage.Nodes.TryGetValue(chosenNodeId, out var node))
                return node;

            return null;
        }
    }
}
