using DecisionHelper.Domain.Models;
using Newtonsoft.Json;

namespace DecisionHelper.Infrastructure.Seeds
{
    internal static class DecisionTreesFilesHelper
    {
        private const string DecisionTreesPath = "DecisionTrees";

        internal static IEnumerable<DecisionTree> GetTrees()
        {
            var treeFiles = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, DecisionTreesPath));
            foreach (var treeFile in treeFiles)
            {
                var treeJson = File.ReadAllText(treeFile);
                var tree = JsonConvert.DeserializeObject<DecisionTree>(treeJson)!;
                yield return tree;
            }
        }
    }
}
