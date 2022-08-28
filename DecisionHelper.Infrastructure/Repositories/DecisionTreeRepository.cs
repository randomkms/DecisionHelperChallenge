using DecisionHelper.Domain.Abstract;
using DecisionHelper.Domain.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace DecisionHelper.Infrastructure.Repositories
{
    public class DecisionTreeRepository : IDecisionTreeRepository
    {
        private const string DecisionTreesPath = "DecisionTrees";
        private readonly ConcurrentDictionary<string, DecisionNode> _trees = new();
        private readonly ConcurrentDictionary<Guid, DecisionNode> _nodes = new();

        public DecisionTreeRepository()
        {
            var treeFiles = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, DecisionTreesPath));
            foreach (var treeFile in treeFiles)
            {
                var treeJson = File.ReadAllText(treeFile); // mb to DB, mb analog of OrderQueries service
                var tree = JsonSerializer.Deserialize<DecisionNode>(treeJson)!;
                _trees.TryAdd(Path.GetFileNameWithoutExtension(treeFile), tree);
                AddTreeToCache(tree);
            }
        }

        public IReadOnlyCollection<string> GetDecisionTrees()
        {
            return _trees.Keys.ToArray();
        }

        public DecisionNode? GetDecisionTree(string treeName)
        {
            if (_trees.TryGetValue(treeName, out var node))
                return node;

            return null;
        }

        public DecisionNode? GetDecisionById(Guid chosenNodeId)
        {
            if (_nodes.TryGetValue(chosenNodeId, out var node))
                return node;

            return null;
        }

        private void AddTreeToCache(DecisionNode node)
        {
            _nodes.TryAdd(node.Id, node);
            foreach (var child in node.Children)
                AddTreeToCache(child);
        }
    }
}
