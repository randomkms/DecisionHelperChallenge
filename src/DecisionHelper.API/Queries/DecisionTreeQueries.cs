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

        public DecisionNodeDto? GetDecisionTree(string treeName)
        {
            var decisionTree = _decisionTreeRepository.GetDecisionTree(treeName);
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
