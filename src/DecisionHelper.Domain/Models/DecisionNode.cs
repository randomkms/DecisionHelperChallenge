namespace DecisionHelper.Domain.Models
{
    public class DecisionNode
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string? Question { get; init; }
        public string? Answer { get; init; }
        public string? Result { get; init; }
        public IReadOnlyList<DecisionNode> Children { get; init; } = Array.Empty<DecisionNode>();
    }
}
