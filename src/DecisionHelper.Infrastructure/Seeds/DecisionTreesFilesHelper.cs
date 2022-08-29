using DecisionHelper.Domain.Models;
using System.Text.Json;

namespace DecisionHelper.Infrastructure.Seeds
{
    internal static class DecisionTreesFilesHelper
    {
        private const string DecisionTreesPath = "DecisionTrees";

        internal static IEnumerable<(string Name, DecisionNode Tree)> GetTrees()
        {
            var treeFiles = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, DecisionTreesPath));
            foreach (var treeFile in treeFiles)
            {
                var treeJson = File.ReadAllText(treeFile);
                var tree = JsonSerializer.Deserialize<DecisionNode>(treeJson)!;
                yield return (Path.GetFileNameWithoutExtension(treeFile), tree);
            }
        }
    }
}
