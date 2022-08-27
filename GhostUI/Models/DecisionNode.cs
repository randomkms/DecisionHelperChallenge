using System.Collections.Generic;
using System;

namespace GhostUI.Models
{
    public class DecisionNode
    {
        public Guid Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? Result { get; set; }
        public List<DecisionNode> Children { get; set; } = new List<DecisionNode>();
    }
}
