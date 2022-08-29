using DecisionHelper.API.Abstract;
using DecisionHelper.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DecisionHelper.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DecisionTreeController : ControllerBase
    {
        private readonly IDecisionTreeQueries _decisionTreeQueries;

        public DecisionTreeController(IDecisionTreeQueries decisionTreeService)
        {
            _decisionTreeQueries = decisionTreeService;
        }

        [HttpGet]
        [ActionName("firstDecision")]
        [ProducesResponseType(typeof(DecisionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFirstDecisionAsync([FromQuery] string treeName)
        {
            var result = await _decisionTreeQueries.GetFirstDecisionAsync(treeName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ActionName("decision")]
        [ProducesResponseType(typeof(DecisionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDecisionAsync(Guid decisionId)
        {
            var result = await _decisionTreeQueries.GetDecisionByIdAsync(decisionId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ActionName("decisionTree")]
        [ProducesResponseType(typeof(DecisionNodeDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDecisionTreeAsync(string treeName)
        {
            var result = await _decisionTreeQueries.GetDecisionTreeAsync(treeName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ActionName("decisionTrees")]
        [ProducesResponseType(typeof(IReadOnlyList<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDecisionTreesAsync()
        {
            var result = await _decisionTreeQueries.GetDecisionTreesAsync();
            return Ok(result);
        }
    }
}
