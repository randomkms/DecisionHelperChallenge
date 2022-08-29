using DecisionHelper.API.Models;
using DecisionHelper.Domain.Models;

namespace DecisionHelper.API.Abstract
{
    public interface IDecisionTreeQueries
    {
        public Task<IReadOnlyList<DecisionTreeInfo>> GetDecisionTreesAsync();

        public Task<DecisionDto?> GetFirstDecisionAsync(string treeName);

        public Task<DecisionDto?> GetDecisionByIdAsync(Guid chosenNodeId);

        public Task<DecisionNodeDto?> GetDecisionTreeAsync(string treeName);
    }
}
