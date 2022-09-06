using DecisionHelper.Domain.Models;

namespace DecisionHelper.UnitTests
{
    public static class Helpers
    {
        public const string TestFilesFolder = "Files";
        public const string DoughnutTreeFile = "Doughnut.json";

        internal static DecisionTree GetDecisionTreeFromFile(string fileName)
        {
            var treeJson = File.ReadAllText(Path.Combine(TestFilesFolder, fileName));
            var tree = JsonConvert.DeserializeObject<DecisionTree>(treeJson)!;
            return tree;
        }
    }
}
