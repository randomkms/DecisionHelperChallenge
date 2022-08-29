using DecisionHelper.API.Models;

namespace DecisionHelper.API.Abstract
{
    public interface IDecisionTreeQueries
    {
        public Task<IReadOnlyList<string>> GetDecisionTreesAsync();

        public Task<DecisionDto?> GetFirstDecisionAsync(string treeName);

        public Task<DecisionDto?> GetDecisionByIdAsync(Guid chosenNodeId);

        public Task<DecisionNodeDto?> GetDecisionTreeAsync(string treeName);
    }
}
