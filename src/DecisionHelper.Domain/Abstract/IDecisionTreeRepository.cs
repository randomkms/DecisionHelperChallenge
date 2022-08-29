using DecisionHelper.Domain.Models;

namespace DecisionHelper.Domain.Abstract
{
    public interface IDecisionTreeRepository
    {
        Task<DecisionNode?> GetDecisionByIdAsync(Guid chosenNodeId);
        Task<DecisionNode?> GetDecisionTreeAsync(string treeName);
        Task<IReadOnlyCollection<string>> GetDecisionTreesAsync();
    }
}
