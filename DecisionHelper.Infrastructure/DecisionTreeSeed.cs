using DecisionHelper.Domain.Models;
using DecisionHelper.Infrastructure.Persistence;
using System.Text.Json;

namespace DecisionHelper.Infrastructure
{
    internal class DecisionTreeSeed
    {
        private const string DecisionTreesPath = "DecisionTrees";
        private readonly IStorage _storage;

        internal DecisionTreeSeed(IStorage storage)
        {
            _storage = storage;
        }

        internal void Seed()
        {
            var treeFiles = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, DecisionTreesPath));
            foreach (var treeFile in treeFiles)
            {
                var treeJson = File.ReadAllText(treeFile); // TODO mb to DB
                var tree = JsonSerializer.Deserialize<DecisionNode>(treeJson)!;
                _storage.Trees.Add(Path.GetFileNameWithoutExtension(treeFile), tree);
                AddNodesToDict(tree);
            }
        }

        private void AddNodesToDict(DecisionNode node)
        {
            _storage.Nodes.Add(node.Id, node);
            foreach (var child in node.Children)
                AddNodesToDict(child);
        }
    }
}
