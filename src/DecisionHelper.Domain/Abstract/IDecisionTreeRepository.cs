using DecisionHelper.Domain.Models;

namespace DecisionHelper.Domain.Abstract
{
    public interface IDecisionTreeRepository
    {
        Task<DecisionNode?> GetDecisionByIdAsync(Guid chosenNodeId);
        Task<DecisionNode?> GetDecisionTreeRootAsync(string treeName);
        Task<IReadOnlyCollection<DecisionTreeInfo>> GetDecisionTreesAsync();
    }
}
