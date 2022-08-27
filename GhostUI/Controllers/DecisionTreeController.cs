using GhostUI.Abstract;
using GhostUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GhostUI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DecisionTreeController : ControllerBase
    {
        private readonly IDecisionTreeService _decisionTreeService;

        public DecisionTreeController(IDecisionTreeService decisionTreeService)
        {
            _decisionTreeService = decisionTreeService;
        }

        [HttpGet]
        [Produces(typeof(DecisionDto))]
        public IActionResult FirstDecision([FromQuery] string treeName)
        {
            var result = _decisionTreeService.GetFirstDecision(treeName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Produces(typeof(DecisionDto))]
        public IActionResult Decision(Guid decisionId)
        {
            var result = _decisionTreeService.GetDecisionById(decisionId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Produces(typeof(DecisionNode))]
        public IActionResult DecisionTree(string treeName)
        {
            var result = _decisionTreeService.GetDecisionTree(treeName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Produces(typeof(IReadOnlyList<string>))]
        public IActionResult DecisionTrees()
        {
            var result = _decisionTreeService.GetDecisionTrees();
            return Ok(result);
        }
    }
}
