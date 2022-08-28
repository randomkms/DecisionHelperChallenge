using DecisionHelper.Domain.Models;

namespace DecisionHelper.Infrastructure.Persistence
{
    public interface IStorage
    {
        IDictionary<Guid, DecisionNode> Nodes { get; }
        IDictionary<string, DecisionNode> Trees { get; }
    }
}