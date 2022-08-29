using DecisionHelper.API.Abstract;
using DecisionHelper.API.Models;
using DecisionHelper.Domain.Abstract;
using DecisionHelper.Domain.Models;

namespace DecisionHelper.API.Services
{
    public class DecisionTreeQueries : IDecisionTreeQueries
    {
        private readonly IDecisionTreeRepository _decisionTreeRepository;

        public DecisionTreeQueries(IDecisionTreeRepository decisionTreeRepository)
        {
            _decisionTreeRepository = decisionTreeRepository;
        }

        public async Task<IReadOnlyList<string>> GetDecisionTreesAsync()
        {
            return (await _decisionTreeRepository.GetDecisionTreesAsync())
                .OrderBy(t => t) //TODO mb move to repo
                .ToArray();
        }

        public async Task<DecisionDto?> GetFirstDecisionAsync(string treeName)
        {
            var decisionTree = await _decisionTreeRepository.GetDecisionTreeAsync(treeName);
            return decisionTree == null ? null : MapNodeToDecisionDto(decisionTree);
        }

        public async Task<DecisionDto?> GetDecisionByIdAsync(Guid chosenNodeId)
        {
            var decision = await _decisionTreeRepository.GetDecisionByIdAsync(chosenNodeId);
            return decision == null ? null : MapNodeToDecisionDto(decision);
        }

        public async Task<DecisionNodeDto?> GetDecisionTreeAsync(string treeName)
        {
            var decisionTree = await _decisionTreeRepository.GetDecisionTreeAsync(treeName);
            return decisionTree == null ? null : MapNodeToDecisionNodeDto(decisionTree);
        }

        private static DecisionDto MapNodeToDecisionDto(DecisionNode node)// TODO mb move to mapper
        {
            return new DecisionDto(node.Id, node.Question, node.Result, node.Children
                .Select(n => MapNodeToPossibleAnswerDto(n)).ToArray());
        }

        private static PossibleAnswerDto MapNodeToPossibleAnswerDto(DecisionNode node)
        {
            return new PossibleAnswerDto(node.Id, node.Answer);
        }

        private static DecisionNodeDto MapNodeToDecisionNodeDto(DecisionNode node)
        {
            return new DecisionNodeDto(node.Id, node.Question, node.Answer, node.Result,
                node.Children.Select(childNode => MapNodeToDecisionNodeDto(childNode)).ToArray());
        }
    }
}
