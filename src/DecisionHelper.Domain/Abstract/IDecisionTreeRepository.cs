using DecisionHelper.Domain.Models;

namespace DecisionHelper.Domain.Abstract
{
    public interface IDecisionTreeRepository
    {
        DecisionNode? GetDecisionById(Guid chosenNodeId);
        DecisionNode? GetDecisionTree(string treeName);
        IReadOnlyCollection<string> GetDecisionTrees();
    }
}
