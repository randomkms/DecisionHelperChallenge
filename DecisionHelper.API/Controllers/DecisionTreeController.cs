using DecisionHelper.API.Abstract;
using DecisionHelper.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DecisionHelper.API.Controllers
{
    [Route("api/[controller]/[action]")]//TODO mb change routing
    [ApiController]
    public class DecisionTreeController : ControllerBase
    {
        private readonly IDecisionTreeQueries _decisionTreeQueries;

        public DecisionTreeController(IDecisionTreeQueries decisionTreeService)
        {
            _decisionTreeQueries = decisionTreeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(DecisionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult FirstDecision([FromQuery] string treeName)
        {
            var result = _decisionTreeQueries.GetFirstDecision(treeName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(DecisionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Decision(Guid decisionId)
        {
            var result = _decisionTreeQueries.GetDecisionById(decisionId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(DecisionNodeDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult DecisionTree(string treeName)
        {
            var result = _decisionTreeQueries.GetDecisionTree(treeName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<string>), (int)HttpStatusCode.OK)]
        public IActionResult DecisionTrees()
        {
            var result = _decisionTreeQueries.GetDecisionTrees();
            return Ok(result);
        }
    }
}
