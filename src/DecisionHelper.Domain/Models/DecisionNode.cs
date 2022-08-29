namespace DecisionHelper.Domain.Models
{
    public record DecisionNode
    {
        public DecisionNode(string? question, string? answer, string? result, IReadOnlyList<DecisionNode> children)
        {
            Question = question;
            Answer = answer;
            Result = result;
            Children = children;
        }

        public Guid Id { get; init; } = Guid.NewGuid();
        public string? Question { get; init; }
        public string? Answer { get; init; }
        public string? Result { get; init; }
        public IReadOnlyList<DecisionNode> Children { get; init; }
    }
}
