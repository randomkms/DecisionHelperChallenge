using DecisionHelper.API.Models;
using DecisionHelper.Domain.Models;

namespace DecisionHelper.API.Abstract
{
    public interface IDecisionTreeService
    {
        public IReadOnlyList<string> GetDecisionTrees();

        public DecisionDto? GetFirstDecision(string treeName);

        public DecisionDto? GetDecisionById(Guid chosenNodeId);

        public DecisionNode? GetDecisionTree(string treeName);
    }
}
