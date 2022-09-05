namespace DecisionHelper.Domain.Models
{
    public class DecisionNode
    {
        private IReadOnlyList<DecisionNode> _children = Array.Empty<DecisionNode>();

        public Guid Id { get; init; } = Guid.NewGuid();
        public string? Question { get; init; }
        public string? Answer { get; init; }
        public string? Result { get; init; }
        public IReadOnlyList<DecisionNode> Children { get => _children; init => _children = value; }

        public DecisionNode CloneWithOnlyDirectChildren()
        {
            var onlyDirectChildrenCopy = this.Children.Select(c =>
            {
                var childClone = c.Clone();
                childClone._children = Array.Empty<DecisionNode>();
                return childClone;
            }).ToArray();

            var nodeClone = this.Clone();
            nodeClone._children = onlyDirectChildrenCopy;

            return nodeClone;
        }

        private DecisionNode Clone()
        {
            return new DecisionNode
            {
                Id = this.Id,
                Question = this.Question,
                Answer = this.Answer,
                Result = this.Result,
                Children = this.Children
            };
        }
    }
}
