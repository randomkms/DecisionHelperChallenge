using DecisionHelper.Domain.Models;
using DecisionHelper.Infrastructure.Persistence;

namespace DecisionHelper.Infrastructure.Seeds
{
    internal class InMemoryDecisionTreeSeed : IDecisionTreeSeed
    {
        private readonly IInMemoryStorage _storage;

        internal InMemoryDecisionTreeSeed(IInMemoryStorage storage)
        {
            _storage = storage;
        }

        public Task SeedAsync()
        {
            foreach (var (name, tree) in DecisionTreesFilesHelper.GetTrees())
            {
                _storage.Trees.Add(name, tree);
                AddNodesToDict(tree);
            }

            return Task.CompletedTask;
        }

        private void AddNodesToDict(DecisionNode node)
        {
            _storage.Nodes.Add(node.Id, node);
            foreach (var child in node.Children)
                AddNodesToDict(child);
        }
    }
}
