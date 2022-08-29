using DecisionHelper.Domain.Models;

namespace DecisionHelper.Infrastructure.Persistence
{
    public interface IInMemoryStorage
    {
        IDictionary<Guid, DecisionNode> Nodes { get; }
        IDictionary<string, DecisionNode> Trees { get; }
        IList<DecisionTreeInfo> TreesInfo { get; }
    }
}