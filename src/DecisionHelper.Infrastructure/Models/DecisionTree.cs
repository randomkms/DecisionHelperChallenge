using Newtonsoft.Json;

namespace DecisionHelper.Domain.Models
{
    internal class DecisionTree : DecisionTreeInfo
    {
        [JsonConstructor]
        internal DecisionTree(string name, DecisionNode root) : base(name)
        {
            Root = root;
        }

        internal DecisionNode Root { get; }

        internal DecisionTreeInfo ToDecisionTreeInfo()
        {
            return new DecisionTreeInfo(Name)
            {
                ImageUrl = ImageUrl,
                Description = Description
            };
        }
    }
}
