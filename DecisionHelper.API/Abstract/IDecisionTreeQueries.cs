using DecisionHelper.API.Models;

namespace DecisionHelper.API.Abstract
{
    public interface IDecisionTreeQueries
    {
        public IReadOnlyList<string> GetDecisionTrees();

        public DecisionDto? GetFirstDecision(string treeName);

        public DecisionDto? GetDecisionById(Guid chosenNodeId);

        public DecisionNodeDto? GetDecisionTree(string treeName);
    }
}
