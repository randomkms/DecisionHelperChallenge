namespace DecisionHelper.Domain.Models
{
    public class DecisionTreeInfo
    {
        public DecisionTreeInfo(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string? Description { get; init; }

        public string? ImageUrl { get; init; }
    }
}
