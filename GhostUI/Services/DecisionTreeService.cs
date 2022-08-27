using GhostUI.Abstract;
using GhostUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GhostUI.Services
{
    public class DecisionTreeService : IDecisionTreeService
    {
        private const string DecisionTreesPath = "DecisionTrees";
        private readonly Dictionary<string, DecisionNode> _trees = new();
        private readonly Dictionary<Guid, DecisionNode> _nodes = new();

        public DecisionTreeService()
        {
            var treeFiles = Directory.GetFiles(DecisionTreesPath);
            foreach (var treeFile in treeFiles)
            {
                var treeJson = File.ReadAllText(treeFile); // TODO move to Repo
                var tree = JsonSerializer.Deserialize<DecisionNode>(treeJson)!;
                _trees.Add(Path.GetFileNameWithoutExtension(treeFile), tree);
                AddTreeToCache(tree);
            }
        }

        public DecisionDto? GetFirstDecision(string treeName)
        {
            if (_trees.TryGetValue(treeName, out var node))
                return MapNodeToDecisionDto(node);

            return null;
        }

        public DecisionDto? GetDecisionById(Guid chosenNodeId)
        {
            if (!_nodes.TryGetValue(chosenNodeId, out var node))
                return null;

            return MapNodeToDecisionDto(node);
        }

        public DecisionNode? GetDecisionTree(string treeName)
        {
            if (_trees.TryGetValue(treeName, out var node))
                return node;

            return null;
        }

        public IReadOnlyList<string> GetDecisionTrees()
        {
            return Directory.GetFiles(DecisionTreesPath)
                .Select(filePath => Path.GetFileNameWithoutExtension(filePath))
                .ToArray();
        }

        private void AddTreeToCache(DecisionNode node)
        {
            _nodes.Add(node.Id, node);
            foreach (var child in node.Children)
                AddTreeToCache(child);
        }

        private static DecisionDto MapNodeToDecisionDto(DecisionNode node)
        {
            return new DecisionDto(node.Id, node.Question, node.Result, node.Children.Select(n => MapNodeToPossibleAnswerDto(n)).ToArray());
        }

        private static PossibleAnswerDto MapNodeToPossibleAnswerDto(DecisionNode node)
        {
            return new PossibleAnswerDto(node.Id, node.Answer);
        }
    }
}
