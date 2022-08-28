using DecisionHelper.API.Abstract;
using DecisionHelper.API.Models;
using DecisionHelper.Domain.Abstract;
using DecisionHelper.Domain.Models;

namespace DecisionHelper.API.Services
{
    public class DecisionTreeService : IDecisionTreeService
    {
        private readonly IDecisionTreeRepository _decisionTreeRepository;

        public DecisionTreeService(IDecisionTreeRepository decisionTreeRepository)
        {
            _decisionTreeRepository = decisionTreeRepository;
        }

        public IReadOnlyList<string> GetDecisionTrees()
        {
            return _decisionTreeRepository.GetDecisionTrees()
                .OrderBy(t => t) //TODO mb move to repo
                .ToArray();
        }

        public DecisionDto? GetFirstDecision(string treeName)
        {
            var decisionTree = _decisionTreeRepository.GetDecisionTree(treeName);
            return decisionTree == null ? null : MapNodeToDecisionDto(decisionTree);
        }

        public DecisionDto? GetDecisionById(Guid chosenNodeId)
        {
            var decision = _decisionTreeRepository.GetDecisionById(chosenNodeId);
            return decision == null ? null : MapNodeToDecisionDto(decision);
        }

        public DecisionNode? GetDecisionTree(string treeName)
        {
            return _decisionTreeRepository.GetDecisionTree(treeName); //TODO move to DTO
        }

        private static DecisionDto MapNodeToDecisionDto(DecisionNode node)
        {
            return new DecisionDto(node.Id, node.Question, node.Result, node.Children
                .Select(n => MapNodeToPossibleAnswerDto(n)).ToArray());
        }

        private static PossibleAnswerDto MapNodeToPossibleAnswerDto(DecisionNode node)
        {
            return new PossibleAnswerDto(node.Id, node.Answer);
        }
    }
}
