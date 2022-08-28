using DecisionHelper.API.Abstract;
using DecisionHelper.API.Models;
using DecisionHelper.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DecisionHelper.API.Controllers
{
    [Route("api/[controller]/[action]")]//TODO mb change routing
    [ApiController]
    public class DecisionTreeController : ControllerBase
    {
        private readonly IDecisionTreeService _decisionTreeService;

        public DecisionTreeController(IDecisionTreeService decisionTreeService)
        {
            _decisionTreeService = decisionTreeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(DecisionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult FirstDecision([FromQuery] string treeName)
        {
            var result = _decisionTreeService.GetFirstDecision(treeName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(DecisionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Decision(Guid decisionId)
        {
            var result = _decisionTreeService.GetDecisionById(decisionId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(DecisionNode), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult DecisionTree(string treeName)
        {
            var result = _decisionTreeService.GetDecisionTree(treeName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<string>), (int)HttpStatusCode.OK)]
        public IActionResult DecisionTrees()
        {
            var result = _decisionTreeService.GetDecisionTrees();
            return Ok(result);
        }
    }
}
