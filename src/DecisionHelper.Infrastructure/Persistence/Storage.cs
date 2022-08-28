using DecisionHelper.Domain.Models;
using System.Collections.Concurrent;

namespace DecisionHelper.Infrastructure.Persistence
{
    internal class Storage : IStorage
    {
        public IDictionary<string, DecisionNode> Trees { get; } = new ConcurrentDictionary<string, DecisionNode>();
        public IDictionary<Guid, DecisionNode> Nodes { get; } = new ConcurrentDictionary<Guid, DecisionNode>();
    }
}
