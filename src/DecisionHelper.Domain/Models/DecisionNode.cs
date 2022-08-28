namespace DecisionHelper.Domain.Models
{
    public record DecisionNode
    {
        public DecisionNode(Guid id, string? question, string? answer, string? result, IReadOnlyList<DecisionNode> children)
        {
            Id = id;
            Question = question;
            Answer = answer;
            Result = result;
            Children = children;
        }

        public Guid Id { get; init; }
        public string? Question { get; init; }
        public string? Answer { get; init; }
        public string? Result { get; init; }
        public IReadOnlyList<DecisionNode> Children { get; init; }
    }
}
