using System;
using System.Collections.Generic;
using GhostUI.Models;

namespace GhostUI.Abstract
{
    public interface IDecisionTreeService
    {
        public DecisionDto? GetFirstDecision(string treeName);

        public DecisionDto? GetDecisionById(Guid chosenNodeId);

        public DecisionNode? GetDecisionTree(string treeName);

        public IReadOnlyList<string> GetDecisionTrees();
    }
}
